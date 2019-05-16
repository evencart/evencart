using EvenCart.Infrastructure.Mvc.Models;

namespace EvenCart.Models.Purchases
{
    public class UpdateCartModel : FoundationModel
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