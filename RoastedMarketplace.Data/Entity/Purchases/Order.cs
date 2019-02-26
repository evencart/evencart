using System;
using System.Collections.Generic;
using RoastedMarketplace.Core.Data;
using RoastedMarketplace.Data.Entity.Addresses;
using RoastedMarketplace.Data.Entity.Payments;
using RoastedMarketplace.Data.Entity.Users;

namespace RoastedMarketplace.Data.Entity.Purchases
{
    public class Order : FoundationEntity
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

        public string TaxDetails { get; set; }

        public decimal OrderTotal { get; set; }

        public string UserGstNumber { get; set; }

        public string UserIpAddress { get; set; }

        public string CurrencyCode { get; set; }

        public OrderStatus OrderStatus { get; set; }

        public PaymentStatus PaymentStatus { get; set; }

        #region Virtual Properties

        public virtual User User { get; set; }

        public virtual IList<OrderItem> OrderItems { get; set; }

        public virtual Address BillingAddress { get; set; }

        public virtual Address ShippingAddress { get; set; }

        public virtual IList<Shipment> Shipments { get; set; }
        
        #endregion

    }
}