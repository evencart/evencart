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

using System.Collections.Generic;
using EvenCart.Core.Data;
using EvenCart.Data.Entity.Addresses;
using EvenCart.Data.Entity.Promotions;
using EvenCart.Data.Entity.Users;

namespace EvenCart.Data.Entity.Purchases
{
    public class Cart : FoundationEntity
    {
        public int UserId { get; set; }

        public int DiscountCouponId { get; set; }

        public int BillingAddressId { get; set; }

        public int ShippingAddressId { get; set; }

        public string PaymentMethodName { get; set; }

        public string PaymentMethodDisplayName { get; set; }

        public string ShippingMethodName { get; set; }

        public string ShippingMethodDisplayName { get; set; }

        public string ShippingOptionsSerialized { get; set; }

        public string SelectedShippingOption { get; set; }

        public bool IsWishlist { get; set; }

        public string PaymentMethodData { get; set; }

        public decimal ShippingFee { get; set; }

        public decimal PaymentMethodFee { get; set; }

        public decimal FinalAmount { get; set; }

        public decimal CompareFinalAmount { get; set; }

        public decimal Discount { get; set; }

        public bool UseStoreCredits { get; set; }

        #region Virtual Properties

        public virtual IList<CartItem> CartItems { get; set; }

        public virtual DiscountCoupon DiscountCoupon { get; set; }

        public virtual User User { get; set; }

        public virtual Address BillingAddress { get; set; }

        public virtual Address ShippingAddress { get; set; }

        #endregion
    }
}