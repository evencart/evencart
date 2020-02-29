using System;
using System.Collections.Generic;
using EvenCart.Core.Data;
using EvenCart.Data.Entity.Addresses;
using EvenCart.Data.Entity.Payments;
using EvenCart.Data.Entity.Shop;
using EvenCart.Data.Entity.Users;

namespace EvenCart.Data.Entity.Purchases
{
    public class Order : FoundationEntity
    {
        public string OrderNumber { get; set; }

        public string Guid { get; set; }

        public int UserId { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? PaidOn { get; set; }

        public string ShippingMethodName { get; set; }

        public string ShippingMethodDisplayName { get; set; }

        public string SelectedShippingOption { get; set; }

        public string PaymentMethodName { get; set; }

        public string PaymentMethodDisplayName { get; set; }

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

        public decimal ExchangeRate { get; set; }

        public OrderStatus OrderStatus { get; set; }

        public PaymentStatus PaymentStatus { get; set; }

        public string BillingAddressSerialized { get; set; }

        public string ShippingAddressSerialized { get; set; }

        public bool DisableReturns { get;set; }

        public string Remarks { get; set; }

        public bool ManualModeTriggered { get; set; }

        public bool IsSubscription { get; set; }

        public bool IsSubscriptionActive { get; set; }

        public bool UsedStoreCredits { get; set; }

        public decimal StoreCredits { get; set; }

        public decimal StoreCreditAmount { get; set; }

        public int StoreId { get; set; }

        #region Virtual Properties

        public virtual User User { get; set; }

        public virtual IList<OrderItem> OrderItems { get; set; }

        public virtual IList<Shipment> Shipments { get; set; }
        
        #endregion

    }
}