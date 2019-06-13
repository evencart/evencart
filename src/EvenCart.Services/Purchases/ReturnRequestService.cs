using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DotEntity;
using DotEntity.Enumerations;
using EvenCart.Core.Services;
using EvenCart.Data.Entity.MediaEntities;
using EvenCart.Data.Entity.Pages;
using EvenCart.Data.Entity.Purchases;
using EvenCart.Data.Entity.Shop;
using EvenCart.Data.Entity.Users;
using EvenCart.Data.Extensions;

namespace EvenCart.Services.Purchases
{
    public class ReturnRequestService : FoundationEntityService<ReturnRequest>, IReturnRequestService
    {
        public override ReturnRequest Get(int id)
        {
            return GetWithJoin(true).Where(x => x.Id == id).SelectNested().FirstOrDefault();
        }

        public IEnumerable<ReturnRequest> GetWithOrderDetails(out int totalResults, IList<ReturnRequestStatus> status = null, DateTime? startDate = null,
            DateTime? endDate = null, int page = 1, int count = Int32.MaxValue)
        {
            var query = GetWithJoin();
            if (status?.Any() ?? false)
            {
                query = query.Where(x => status.Contains(x.ReturnRequestStatus));
            }

            if (startDate.HasValue)
            {
                query = query.Where(x => x.CreatedOn >= startDate);
            }
            if (endDate.HasValue)
            {
                query = query.Where(x => x.CreatedOn <= endDate);
            }

            query = query.OrderBy(x => x.CreatedOn, RowOrder.Descending);
            return query.SelectNestedWithTotalMatches(out totalResults, page, count);
        }

        public IEnumerable<ReturnRequest> GetOrderReturnRequests(int orderId)
        {
            Expression<Func<SeoMeta, bool>> where = meta => meta.EntityName == nameof(Product);
            return GetWithJoin(true)
                .Join<SeoMeta>("Id", "EntityId", typeof(Product), JoinType.LeftOuter)
                .Relate<SeoMeta>((request, meta) => { request.OrderItem.Product.SeoMeta = meta; })
                .Where(x => x.OrderId == orderId)
                .Where(where)
                .SelectNested();
        }

        private IEntitySet<ReturnRequest> GetWithJoin(bool withReturnOrder = false)
        {
            var query = Repository.Join<Order>("OrderId", "Id", joinType: JoinType.LeftOuter)
                .Join<OrderItem>("OrderItemId", "Id", SourceColumn.Parent, JoinType.LeftOuter)
                .Join<Product>("ProductId", "Id", typeof(OrderItem))
                .Join<ProductMedia>("Id", "ProductId", joinType: JoinType.LeftOuter)
                .Join<Media>("MediaId", "Id", joinType: JoinType.LeftOuter)
                .Join<User>("UserId", "Id", SourceColumn.Parent, JoinType.LeftOuter)
                .Relate<Order>((request, order) =>
                {
                    if (request.OrderId == order.Id)
                        request.Order = order;

                    if (request.ReturnOrderId == order.Id)
                        request.ReturnOrder = order;
                })
                .Relate(RelationTypes.OneToOne<ReturnRequest, OrderItem>((request, item) =>
                {
                    request.Order.OrderItems = request.Order.OrderItems ?? new List<OrderItem>();
                    request.Order.OrderItems.Add(item);
                }))
                .Relate<Product>((request, product) => { request.OrderItem.Product = product; })
                .Relate<Media>((request, media) =>
                {
                    request.OrderItem.Product.MediaItems =
                        request.OrderItem.Product.MediaItems ?? new List<Media>();
                    request.OrderItem.Product.MediaItems.Add(media);
                })
                .Relate(RelationTypes.OneToOne<ReturnRequest, User>());

            if (withReturnOrder)
            {
                query = query.Join<Order>("ReturnOrderId", "Id", SourceColumn.Parent, JoinType.LeftOuter);
            }
            return query;
        }
    }
}