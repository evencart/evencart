using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DotEntity;
using DotEntity.Enumerations;
using RoastedMarketplace.Core.Services;
using RoastedMarketplace.Data.Entity.Addresses;
using RoastedMarketplace.Data.Entity.MediaEntities;
using RoastedMarketplace.Data.Entity.Pages;
using RoastedMarketplace.Data.Entity.Payments;
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
            return Repository.Join<OrderItem>("Id", "OrderId", joinType: JoinType.LeftOuter)
                .Join<Product>("ProductId", "Id", joinType: JoinType.LeftOuter)
                .Join<User>("UserId", "Id", SourceColumn.Parent, JoinType.LeftOuter)
                .Join<Address>("Id", "UserId", joinType: JoinType.LeftOuter)
                .Relate(RelationTypes.OneToMany<Order, OrderItem>())
                .Relate(RelationTypes.OneToOne<Order, User>())
                .Relate<Address>((order, address) =>
                {
                    if (order.BillingAddressId == address.Id)
                        order.BillingAddress = address;
                    if (order.ShippingAddressId == address.Id)
                        order.ShippingAddress = address;
                })
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

            if (productIds?.Any() ?? false)
            {
                query = query.Join<OrderItem>("Id", "OrderId", joinType: JoinType.LeftOuter);
                Expression<Func<OrderItem, bool>> whereProductIdMatches = (orderItem) => productIds.Contains(orderItem.ProductId);
                query = query.Where(whereProductIdMatches);
            }
            if (!productName.IsNullEmptyOrWhiteSpace() || (vendorIds?.Any() ?? false))
            {
                if (productIds == null || !productIds.Any())
                {
                    query = query.Join<OrderItem>("Id", "OrderId", joinType: JoinType.LeftOuter);
                }
                query = query.Join<Product>("ProductId", "Id", joinType: JoinType.LeftOuter);
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
                .Join<Address>("UserId", "UserId", SourceColumn.Parent, joinType: JoinType.LeftOuter)
                .Join<Country>("CountryId", "Id", SourceColumn.Chained, JoinType.LeftOuter)
                .Join<StateOrProvince>("CountryId", "Id", typeof(Address), JoinType.LeftOuter)
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
                .Relate<Address>((order, address) =>
                {
                    if (order.BillingAddressId == address.Id)
                        order.BillingAddress = address;
                    if (order.ShippingAddressId == address.Id)
                        order.ShippingAddress = address;
                })
                .Relate<Country>((order, country) =>
                {
                    if (order.BillingAddress.CountryId == country.Id)
                        order.BillingAddress.Country = country;

                    if (order.ShippingAddress.CountryId == country.Id)
                        order.ShippingAddress.Country = country;
                })
                .Relate<StateOrProvince>((order, province) =>
                {
                    if (order.BillingAddress.StateProvinceId == province.Id)
                        order.BillingAddress.StateOrProvince = province;

                    if (order.ShippingAddress.StateProvinceId == province.Id)
                        order.ShippingAddress.StateOrProvince = province;
                })
                .Where(meteWhere);
        }
    }
}