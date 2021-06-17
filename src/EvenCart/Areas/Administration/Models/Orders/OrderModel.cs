#region License
// Copyright (c) Sojatia Infocrafts Private Limited.
// The following code is part of EvenCart eCommerce Software (https://evencart.co) Dual Licensed under the terms of
// 
// 1. GNU GPLv3 with additional terms (available to read at https://evencart.co/license)
// 2. EvenCart Proprietary License (available to read at https://evencart.co/license/commercial-license).
// 
// You can select one of the above two licenses according to your requirements. The usage of this code is
// subject to the terms of the license chosen by you.
#endregion

using System;
using System.Collections.Generic;
using EvenCart.Areas.Administration.Models.Addresses;
using EvenCart.Areas.Administration.Models.Users;
using EvenCart.Data.Entity.Payments;
using EvenCart.Data.Entity.Purchases;
using Genesis.Infrastructure.Mvc.Attributes;
using Genesis.Infrastructure.Mvc.Models;
using Genesis.Modules.Data;

namespace EvenCart.Areas.Administration.Models.Orders
{
    [FormatAsCurrencies(nameof(OrderTotal), nameof(Subtotal), nameof(Discount), nameof(ShippingMethodFee), nameof(PaymentMethodFee), nameof(Tax), CurrencyCodeProperty = nameof(CurrencyCode))]
    public class OrderModel : GenesisEntityModel
    {
        public string OrderNumber { get; set; }

        public string Guid { get; set; }

        public int UserId { get; set; }

        public int BillingAddressId { get; set; }

        public int? ShippingAddressId { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? PaidOn { get; set; }

        public DateTime? DeliveredOn { get; set; }

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

        public decimal OrderTotal { get; set; }
        [Obsolete]
        public string OrderTotalFormatted => OrderTotal.ToCurrency(CurrencyCode);

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

        public bool IsSubscription { get; set; }

        public bool IsSubscriptionActive { get; set; }
        
        public DateTime? LastInvoiceDate { get; set; }
        
        public DateTime? NextInvoiceDate { get; set; }

        public virtual IList<ShippingOptionModel> SelectedShippingOptions { get; set; }
    }
}