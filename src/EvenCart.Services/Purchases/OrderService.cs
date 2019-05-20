using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using DotEntity;
using DotEntity.Enumerations;
using EvenCart.Core.Services;
using EvenCart.Data.Entity.Addresses;
using EvenCart.Data.Entity.MediaEntities;
using EvenCart.Data.Entity.Pages;
using EvenCart.Data.Entity.Payments;
using EvenCart.Data.Entity.Purchases;
using EvenCart.Data.Entity.Shop;
using EvenCart.Data.Entity.Users;
using EvenCart.Data.Extensions;

namespace EvenCart.Services.Purchases
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
            return Repository.Join<OrderItem>("Id", "OrderId", joinType: JoinType.LeftOuter)
                .Join<Product>("ProductId", "Id", joinType: JoinType.LeftOuter)
                .Join<User>("UserId", "Id", SourceColumn.Parent, JoinType.LeftOuter)
                .Relate(RelationTypes.OneToMany<Order, OrderItem>())
                .Relate(RelationTypes.OneToOne<Order, User>())
                .Relate<Product>((order, product) =>
                {
                    var orderItem = order.OrderItems.FirstOrDefault(x => x.ProductId == product.Id);
                    if (orderItem != null)
                        orderItem.Product = product;
                })
                .Where(x => x.Id == id)
                .SelectNested()
                .FirstOrDefault();
        }

        public IEnumerable<Order> GetOrders(out int totalResults, string productName = null, int? userId = null, IList<int> orderIds = null, IList<int> productIds = null, IList<OrderStatus> orderStatus = null, IList<PaymentStatus> paymentStatus = null, IList<int> vendorIds = null, DateTime? startDate = null, DateTime? endDate = null, int page = 1, int count = int.MaxValue)
        {
            var query = Repository;
            //check one by one for each filter
            if (userId.HasValue)
            {
                query = query.Where(x => x.UserId == userId);
            }
            if (orderIds != null && orderIds.Any())
            {
                query = query.Where(x => orderIds.Contains(x.Id));
            }
            //date filters
            if (startDate.HasValue)
            {
                query = query.Where(x => x.CreatedOn >= startDate);
            }
            if (endDate.HasValue)
            {
                query = query.Where(x => x.CreatedOn <= endDate);
            }

            query = query.Join<OrderItem>("Id", "OrderId", joinType: JoinType.LeftOuter)
                .Relate(RelationTypes.OneToMany<Order, OrderItem>());

            query = query.Join<Product>("ProductId", "Id", joinType: JoinType.LeftOuter)
                .Relate<Product>((order, product) =>
                {
                    foreach (var orderItem in order.OrderItems)
                    {
                        if (orderItem.ProductId == product.Id)
                            orderItem.Product = product;
                    }
                });

            if (productIds?.Any() ?? false)
            {
                Expression<Func<OrderItem, bool>> whereProductIdMatches = (orderItem) => productIds.Contains(orderItem.ProductId);
                query = query.Where(whereProductIdMatches);
            }
            if (!productName.IsNullEmptyOrWhiteSpace() || (vendorIds?.Any() ?? false))
            {
                
                if (vendorIds?.Any() ?? false)
                {
                    Expression<Func<ProductVendor, bool>> whereVendorIdMatches = (vendorItem) => vendorIds.Contains(vendorItem.VendorId);
                    query = query.Join<ProductVendor>("Id", "ProductId", joinType: JoinType.LeftOuter)
                        .Where(whereVendorIdMatches);
                }

                if (!productName.IsNullEmptyOrWhiteSpace())
                {
                    Expression<Func<Product, bool>> whereProductName = product => product.Name.Contains(productName);
                    query = query.Where(whereProductName);
                }
            }

            if (orderStatus != null && orderStatus.Any())
            {
                query = query.Where(x => orderStatus.Contains(x.OrderStatus));
            }

            if (paymentStatus != null && paymentStatus.Any())
            {
                query = query.Where(x => paymentStatus.Contains(x.PaymentStatus));
            }

            query = query.Join<User>("UserId", "Id", SourceColumn.Parent, JoinType.LeftOuter).Relate(RelationTypes.OneToOne<Order, User>());
            ; query = query.OrderBy(x => x.CreatedOn, RowOrder.Descending);
            var orders = query.SelectNestedWithTotalMatches(out totalResults, page, count);
            return orders;
        }

        public Order GetByGuid(string guid)
        {
            return GetByWhere(x => x.Guid == guid)
                .SelectNested()
                .FirstOrDefault();
        }

        public Dictionary<OrderStatus, int> GetOrderCountsByStatus()
        {
            var tableName = DotEntityDb.GetTableNameForType<Order>();
            var enclosedTableName = DotEntityDb.Provider.SafeEnclose(tableName);
            var orderStatusCounts = new Dictionary<OrderStatus, int>();
            foreach (OrderStatus status in System.Enum.GetValues(typeof(OrderStatus)))
            {
                orderStatusCounts.Add(status, 0);
            }
            using (var result = EntitySet.Query($"SELECT OrderStatus, COUNT(*) AS OrderCount FROM {enclosedTableName} GROUP BY OrderStatus", null))
            {
                var totals = result.SelectAllAs<OrderStatusTotal>().ToList();
                foreach (var t in totals)
                    orderStatusCounts[t.OrderStatus] = t.OrderCount;
            }

            return orderStatusCounts;
        }

        public override IEnumerable<Order> Get(out int totalResults, Expression<Func<Order, bool>> @where, Expression<Func<Order, object>> orderBy = null,
            RowOrder rowOrder = RowOrder.Ascending, int page = 1, int count = Int32.MaxValue)
        {

            var query = GetByWhere(where);
            return query.OrderBy(orderBy, rowOrder)
                .SelectNestedWithTotalMatches(out totalResults, page, count);

        }

        private IEntitySet<Order> GetByWhere(Expression<Func<Order, bool>> where)
        {
            Expression<Func<SeoMeta, bool>> meteWhere = x => x.EntityName == "Product";
            return Repository.Where(where)
                .Join<OrderItem>("Id", "OrderId", joinType: JoinType.LeftOuter)
                .Join<ShipmentItem>("Id", "OrderItemId", joinType: JoinType.LeftOuter)
                .Join<Shipment>("ShipmentId", "Id", joinType: JoinType.LeftOuter)
                .Join<User>("UserId", "Id", SourceColumn.Parent, JoinType.LeftOuter)
                .Join<Product>("ProductId", "Id", typeof(OrderItem))
                .Join<ProductMedia>("Id", "ProductId", joinType: JoinType.LeftOuter)
                .Join<Media>("MediaId", "Id", joinType: JoinType.LeftOuter)
                .Join<SeoMeta>("Id", "EntityId", typeof(Product))
                .Relate(RelationTypes.OneToMany<Order, OrderItem>())
                .Relate(RelationTypes.OneToMany<Order, Shipment>())
                .Relate(RelationTypes.OneToOne<Order, User>())
                .Relate<ShipmentItem>((order, shipmentItem) =>
                {
                    var orderItem = order.OrderItems.First(x => x.Id == shipmentItem.OrderItemId);
                    orderItem.Shipment = order.Shipments.First(x => x.Id == shipmentItem.ShipmentId);
                    orderItem.Shipment.ShipmentItems = orderItem.Shipment.ShipmentItems ?? new List<ShipmentItem>();
                    if (!orderItem.Shipment.ShipmentItems.Contains(shipmentItem))
                        orderItem.Shipment.ShipmentItems.Add(shipmentItem);
                })
                .Relate<Product>((order, product) =>
                {
                    foreach (var orderItem in order.OrderItems)
                    {
                        if (orderItem.ProductId == product.Id)
                            orderItem.Product = product;
                    }
                })
                .Relate<ProductMedia>((order, media) =>
                {
                    foreach (var orderItem in order.OrderItems)
                    {
                        if (orderItem.ProductId == media.ProductId)
                            orderItem.Product.Tag = media.MediaId;
                    }
                })
                .Relate<Media>((order, media) =>
                {
                    foreach (var orderItem in order.OrderItems)
                    {
                        if ((int) orderItem.Product.Tag == media.Id)
                        {
                            orderItem.Product.MediaItems = orderItem.Product.MediaItems ?? new List<Media>();
                            if (!orderItem.Product.MediaItems.Contains(media))
                                orderItem.Product.MediaItems.Add(media);
                        }
                    }
                })
                .Relate<SeoMeta>((order, meta) =>
                {
                    var orderItem = order.OrderItems.FirstOrDefault(x => x.ProductId == meta.EntityId);
                    if (orderItem != null)
                        orderItem.Product.SeoMeta = meta;
                })
                .Where(meteWhere);
        }


        private class OrderStatusTotal
        {
            public OrderStatus OrderStatus { get; set; }

            public int OrderCount { get; set; }
        }
    }
}