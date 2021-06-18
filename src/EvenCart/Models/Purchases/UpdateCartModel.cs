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

using Genesis.Infrastructure.Mvc.Models;

namespace EvenCart.Models.Purchases
{
    public class UpdateCartModel : GenesisModel
    {
        /// <summary>
        /// The discount coupon to be applied to the cart
        /// </summary>
        public string DiscountCoupon { get; set; }

        /// <summary>
        /// The gift card to be applied to the cart
        /// </summary>
        public string GiftCode { get; set; }

        /// <summary>
        /// If a cart item is being updated, the id of cart item
        /// </summary>
        public int? CartItemId { get; set; }

        /// <summary>
        /// The updated quantity of the cart item to be updated
        /// </summary>
        public int? Quantity { get; set; }

        /// <summary>
        /// Set to true if an already applied coupon needs to be removed from the cart
        /// </summary>
        public bool RemoveCoupon { get; set; }

        /// <summary>
        /// Set to true if updates are being done on wishlist. Set to false if updates are being done on cart
        /// </summary>
        public bool IsWishlist { get; set; }
    }
}