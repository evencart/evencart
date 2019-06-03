using System;
using System.Collections.Generic;
using EvenCart.Areas.Administration.Models.Addresses;
using EvenCart.Areas.Administration.Models.Users;
using EvenCart.Data.Entity.Payments;
using EvenCart.Data.Entity.Purchases;
using EvenCart.Infrastructure.Extensions;
using EvenCart.Infrastructure.Mvc.Models;

namespace EvenCart.Areas.Administration.Models.Orders
{
    public class OrderModel : FoundationEntityModel
    {
        public string OrderNumber { get; set; }

        public string Guid { get; set; }

        public int UserId { get; set; }

        public int BillingAddressId { get; set; }

        public int? ShippingAddressId { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? PaidOn { get; set; }

        public string ShippingMethodName { get; set; }

        public string PaymentMethodName { get; set; }

        public decimal Discount { get; set; }

        public string DiscountCoupon { get; set; }

        public int? DiscountId { get; set; }

        public decimal Subtotal { get; set; }

        public decimal? ShippingMethodFee { get; set; }

        public decimal? PaymentMethodFee { get; set; }

        public decimal Tax { get; set; }

        public decimal OrderTotal => Subtotal + Tax - Discount + ShippingMethodFee ?? 0 + PaymentMethodFee ?? 0;

        public string OrderTotalFormatted => OrderTotal.ToCurrency();

        public string UserGstNumber { get; set; }

        public string UserIpAddress { get; set; }

        public string CurrencyCode { get; set; }

        public OrderStatus OrderStatus { get; set; }

        public string OrderStatusDisplay => OrderStatus.ToString();

        public PaymentStatus PaymentStatus { get; set; }

        public string PaymentStatusDisplay => PaymentStatus.ToString();

        public IList<OrderItemModel> OrderItems { get; set; }

        public UserModel User { get; set; }

        public AddressModel BillingAddress { get; set; }

        public AddressModel ShippingAddress { get; set; }


    }
}