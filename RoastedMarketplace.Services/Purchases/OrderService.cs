using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DotEntity;
using DotEntity.Enumerations;
using RoastedMarketplace.Core.Services;
using RoastedMarketplace.Data.Entity.Purchases;
using RoastedMarketplace.Data.Entity.Shop;
using RoastedMarketplace.Data.Entity.Users;
using RoastedMarketplace.Data.Extensions;

namespace RoastedMarketplace.Services.Purchases
{
    public class OrderService : FoundationEntityService<Order>, IOrderService
    {
        private readonly IOrderItemService _orderItemService;
        public OrderService(IOrderItemService orderItemService)
        {
            _orderItemService = orderItemService;
        }

        public override Order Get(int id)
        {
            return GetByWhere(x => x.Id == id);
        }

        public IEnumerable<Order> GetOrders(int? userId = null, int[] productIds = null, OrderStatus? orderStatus = null, int[] vendorIds = null,
            DateTime? startDate = null, DateTime? endDate = null, int page = 1, int count = Int32.MaxValue)
        {
            var query = Repository;
            //check one by one for each filter
            if (userId.HasValue)
            {
                query = query.Where(x => x.UserId == userId);
            }
            //date filters
            if (startDate.HasValue)
            {
                query = query.Where(x => x.CreatedOn >= startDate);
            }
            if (endDate.HasValue)
            {
                query = query.Where(x => x.CreatedOn >= endDate);
            }
            var nested = false;
            //join for order item to find out about orders
            if ((productIds?.Any() ?? false) || (vendorIds?.Any() ?? false))
            {
                nested = true;
                query = query.Join<OrderItem>("Id", "OrderId", joinType: JoinType.LeftOuter);
            }
            if (productIds?.Any() ?? false)
            {
                Expression<Func<OrderItem, bool>> whereProductIdMatches = (orderItem) => productIds.Contains(orderItem.ProductId);
                query = query.Where(whereProductIdMatches);
            }
            if (vendorIds?.Any() ?? false)
            {
                Expression<Func<ProductVendor, bool>> whereVendorIdMatches = (vendorItem) => vendorIds.Contains(vendorItem.VendorId);
                query = query.Join<Product>("ProductId", "Id", joinType: JoinType.LeftOuter)
                    .Join<ProductVendor>("Id", "ProductId", joinType: JoinType.LeftOuter)
                    .Where(whereVendorIdMatches);
            }

            if (orderStatus.HasValue)
            {
                query = query.Where(x => x.OrderStatus == orderStatus);
            }

            var orders = nested ? query.SelectNested(page, count) : query.Select(page, count);
            return orders;
        }

        public Order GetByGuid(string guid)
        {
            return GetByWhere(x => x.Guid == guid);
        }

        private Order GetByWhere(Expression<Func<Order, bool>> where)
        {
            return Repository.Where(where)
                .Join<OrderItem>("Id", "OrderId", joinType: JoinType.LeftOuter)
                .Join<ShipmentItem>("OrderItemId", "OrderItemId", joinType: JoinType.LeftOuter)
                .Join<Shipment>("ShipmentId", "Id", joinType: JoinType.LeftOuter)
                .Join<User>("UserId", "Id", SourceColumn.Parent, JoinType.LeftOuter)
                .Relate(RelationTypes.OneToMany<Order, OrderItem>())
                .Relate(RelationTypes.OneToMany<Order, Shipment>())
                .Relate(RelationTypes.OneToOne<Order, User>())
                .Relate<ShipmentItem>((order, shipmentItem) =>
                {
                    var orderItem = order.OrderItems.First(x => x.Id == shipmentItem.OrderItemId);
                    orderItem.Shipment.ShipmentItems = orderItem.Shipment.ShipmentItems ?? new List<ShipmentItem>();
                    if (!orderItem.Shipment.ShipmentItems.Contains(shipmentItem))
                        orderItem.Shipment.ShipmentItems.Add(shipmentItem);
                })
                .SelectNested()
                .FirstOrDefault();
        }
    }
}