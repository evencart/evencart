using System;
using System.Collections.Generic;
using EvenCart.Data.Entity.Payments;
using EvenCart.Data.Entity.Purchases;
using EvenCart.Infrastructure.Mvc.Models;
using EvenCart.Models.Addresses;
using UserModel = EvenCart.Models.Users.UserModel;

namespace EvenCart.Models.Orders
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

        public string UserGstNumber { get; set; }

        public string UserIpAddress { get; set; }

        public string CurrencyCode { get; set; }

        public OrderStatus OrderStatus { get; set; }

        public string OrderStatusDisplay => OrderStatus.ToString();

        public PaymentStatus PaymentStatus { get; set; }

        public string PaymentStatusDisplay => PaymentStatus.ToString();

        public IList<OrderItemModel> OrderItems { get; set; }

        public UserModel User { get; set; }

        public AddressInfoModel BillingAddress { get; set; }

        public AddressInfoModel ShippingAddress { get; set; }

    }
}