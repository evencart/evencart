using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DotEntity;
using DotEntity.Enumerations;
using ImTools;
using RoastedMarketplace.Core.Services;
using RoastedMarketplace.Data.Entity.MediaEntities;
using RoastedMarketplace.Data.Entity.Pages;
using RoastedMarketplace.Data.Entity.Reviews;
using RoastedMarketplace.Data.Entity.Shop;
using RoastedMarketplace.Data.Entity.Users;
using RoastedMarketplace.Data.Extensions;

namespace RoastedMarketplace.Services.Reviews
{
    public class ReviewService : FoundationEntityService<Review>, IReviewService
    {
        public override IEnumerable<Review> Get(Expression<Func<Review, bool>> @where, int page = 1, int count = Int32.MaxValue)
        {
            return GetByWhere(where, review => review.CreatedOn, RowOrder.Descending).SelectNested(page, count);
        }

        public override IEnumerable<Review> Get(out int totalResults, Expression<Func<Review, bool>> @where,
            Expression<Func<Review, object>> orderBy = null, RowOrder rowOrder = RowOrder.Ascending, int page = 1,
            int count = Int32.MaxValue)
        {
            orderBy = orderBy ?? (review => review.CreatedOn); 
            return GetByWhere(where, orderBy, rowOrder)
                .SelectNestedWithTotalMatches(out totalResults, page, count);
        }

        public Review GetBestReview(int productId)
        {
            return GetSingleReview(productId, RowOrder.Descending);
        }

        public Review GetWorstReview(int productId)
        {
            return GetSingleReview(productId, RowOrder.Ascending);
        }

        private Review GetSingleReview(int productId, RowOrder ratingOrder)
        {
            return GetByWhere(x => x.ProductId == productId && x.Published, x => x.Rating, ratingOrder)
                .OrderBy(x => x.CreatedOn, RowOrder.Descending)
                .SelectNested(1, 1)
                .FirstOrDefault();
        }

        private IEntitySet<Review> GetByWhere(Expression<Func<Review, bool>> where, Expression<Func<Review, object>> orderBy, RowOrder rowOrder)
        {
            Expression<Func<Product, bool>> productWhere = product => !product.ReviewsDisabled ;
            Expression<Func<SeoMeta, bool>> seoMetaWhere = meta => meta.EntityName == nameof(Product);
            return Repository.Join<User>("UserId", "Id", joinType: JoinType.LeftOuter)
                .Join<Product>("ProductId", "Id", SourceColumn.Parent, JoinType.LeftOuter)
                .Join<SeoMeta>("Id", "EntityId", typeof(Product), JoinType.LeftOuter)
                .Join<ProductMedia>("Id", "ProductId", typeof(Product), JoinType.LeftOuter)
                .Join<Media>("MediaId", "Id", SourceColumn.Chained, JoinType.LeftOuter)
                .Relate(RelationTypes.OneToOne<Review, User>())
                .Relate(RelationTypes.OneToOne<Review, Product>())
                .Relate<SeoMeta>((review, meta) => { review.Product.SeoMeta = meta; })
                .Relate<Media>((review, media) =>
                    {
                        review.Product.MediaItems = review.Product.MediaItems ?? new List<Media>();
                        if (!review.Product.MediaItems.Contains(media))
                            review.Product.MediaItems.Add(media);
                    })
                .OrderBy(orderBy, rowOrder)
                .Where(where)
                .Where(seoMetaWhere)
                .Where(productWhere);
        }
    }
}