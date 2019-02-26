using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DotEntity.Enumerations;
using RoastedMarketplace.Core.Services;
using RoastedMarketplace.Data.Entity.Reviews;
using RoastedMarketplace.Data.Entity.Users;
using RoastedMarketplace.Data.Extensions;

namespace RoastedMarketplace.Services.Reviews
{
    public class ReviewService : FoundationEntityService<Review>, IReviewService
    {
        public override IEnumerable<Review> Get(Expression<Func<Review, bool>> @where, int page = 1, int count = Int32.MaxValue)
        {
            return Repository.Join<User>("UserId", "Id", joinType: JoinType.LeftOuter)
                .Relate(RelationTypes.OneToOne<Review, User>())
                .OrderBy(x => x.CreatedOn, RowOrder.Descending)
                .SelectNested(page, count);
        }

        public override IEnumerable<Review> Get(out int totalResults, Expression<Func<Review, bool>> @where,
            Expression<Func<Review, object>> orderBy = null, RowOrder rowOrder = RowOrder.Ascending, int page = 1,
            int count = Int32.MaxValue)
        {
            return Repository.Join<User>("UserId", "Id", joinType: JoinType.LeftOuter)
                .Relate(RelationTypes.OneToOne<Review, User>())
                .OrderBy(x => x.CreatedOn, RowOrder.Descending)
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
            return Repository.Where(x => x.ProductId == productId && x.Published)
                .OrderBy(x => x.Rating, ratingOrder)
                .OrderBy(x => x.CreatedOn, RowOrder.Descending)
                .Join<User>("UserId", "Id", joinType: JoinType.LeftOuter)
                .Relate(RelationTypes.OneToOne<Review, User>())
                .SelectNested(1, 1)
                .FirstOrDefault();
        }
    }
}