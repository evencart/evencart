﻿#region License
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
using EvenCart.Data.Entity.Payments;
using EvenCart.Data.Entity.Purchases;
using EvenCart.Models.Addresses;
using EvenCart.Models.Shipments;
using Genesis.Extensions;
using Genesis.Infrastructure.Mvc.Models;
using UserModel = EvenCart.Models.Users.UserModel;

namespace EvenCart.Models.Orders
{
    public class OrderModel : GenesisEntityModel
    {
        /// <summary>
        /// The displayable order number
        /// </summary>
        public string OrderNumber { get; set; }
        /// <summary>
        /// The unique identifier for the order
        /// </summary>
        public string Guid { get; set; }
        /// <summary>
        /// The id of the user
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// Date when order was created
        /// </summary>
        public DateTime CreatedOn { get; set; }
        /// <summary>
        /// Date when the order was created in user's timezone
        /// </summary>
        public DateTime CreatedOnLocal => CreatedOn.ToUserDateTime();
        /// <summary>
        /// Date when order was paid
        /// </summary>
        public DateTime? PaidOn { get; set; }
        /// <summary>
        /// Date when the order was Paid in user's timezone
        /// </summary>
        public DateTime? PaidOnLocal => PaidOn?.ToUserDateTime();
        /// <summary>
        /// The shipping method name used while placing the order
        /// </summary>
        public string ShippingMethodName { get; set; }
        /// <summary>
        /// The shipping method display name
        /// </summary>
        public string ShippingMethodDisplayName { get; set; }
        /// <summary>
        /// The shipping option selected by the user
        /// </summary>
        public string SelectedShippingOption { get; set; }
        /// <summary>
        /// The payment method name used while placing the order
        /// </summary>
        public string PaymentMethodName { get; set; }
        /// <summary>
        /// The payment method name used while placing the order
        /// </summary>
        public string PaymentMethodDisplayName { get; set; }
        /// <summary>
        /// The discount amount for the order
        /// </summary>
        public decimal Discount { get; set; }
        /// <summary>
        /// The discount coupon that was used in the order
        /// </summary>
        public string DiscountCoupon { get; set; }
        /// <summary>
        /// The id of the discount coupon used in the order
        /// </summary>
        public int? DiscountId { get; set; }
        /// <summary>
        /// The subtotal of the order
        /// </summary>
        public decimal Subtotal { get; set; }
        /// <summary>
        /// Any additional fee for shipping method
        /// </summary>
        public decimal? ShippingMethodFee { get; set; }
        /// <summary>
        /// Any additional fee for payment method
        /// </summary>
        public decimal? PaymentMethodFee { get; set; }
        /// <summary>
        /// The tax amount for the order
        /// </summary>
        public decimal Tax { get; set; }
        /// <summary>
        /// The order total amount
        /// </summary>
        public decimal OrderTotal => Subtotal + Tax - Discount + ShippingMethodFee ?? 0 + PaymentMethodFee ?? 0;
        /// <summary>
        /// The GST number provided by user
        /// </summary>
        public string UserGstNumber { get; set; }
        /// <summary>
        /// The IP address used for placing the order
        /// </summary>
        public string UserIpAddress { get; set; }
        /// <summary>
        /// The ISO currency code for the currency used
        /// </summary>
        public string CurrencyCode { get; set; }
        /// <summary>
        /// The order status
        /// </summary>
        public OrderStatus OrderStatus { get; set; }

        public string OrderStatusDisplay => OrderStatus.ToString();
        /// <summary>
        /// The payment status
        /// </summary>
        public PaymentStatus PaymentStatus { get; set; }

        public string PaymentStatusDisplay => PaymentStatus.ToString();
        /// <summary>
        /// List of <see cref="OrderItemModel">orderItems</see> for this order
        /// </summary>
        public IList<OrderItemModel> OrderItems { get; set; }
        /// <summary>
        /// The <see cref="UserModel">user</see> information
        /// </summary>
        public UserModel User { get; set; }
        /// <summary>
        /// The <see cref="AddressInfoModel">billingAddress</see> used while placing the order
        /// </summary>
        public AddressInfoModel BillingAddress { get; set; }

        /// The <see cref="AddressInfoModel">shippingAddress</see> used while placing the order
        public AddressInfoModel ShippingAddress { get; set; }
        /// <summary>
        /// List of <see cref="ShipmentModel">shipment</see> objects for this order
        /// </summary>
        public IList<ShipmentModel> Shipments { get; set; }
        /// <summary>
        /// List of <see cref="OrderTaxModel">tax</see> objects for this order
        /// </summary>
        public IList<OrderTaxModel> Taxes { get; set; }
        /// <summary>
        /// Specifies if the order is a subscription
        /// </summary>
        public bool IsSubscription { get; set; }
        /// <summary>
        /// Specifies if the subscription is active
        /// </summary>
        public bool IsSubscriptionActive { get; set; }

    }
}