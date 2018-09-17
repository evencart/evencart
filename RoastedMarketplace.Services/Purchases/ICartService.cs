using RoastedMarketplace.Core.Services;
using RoastedMarketplace.Data.Entity.Purchases;
using RoastedMarketplace.Services.Promotions;

namespace RoastedMarketplace.Services.Purchases
{
    public interface ICartService : IFoundationEntityService<Cart>
    {
        Cart GetCart(int userId);

        Cart GetWishlist(int userId);

        void AddToCart(int userId, CartItem item);

        void AddToWishlist(int userId, CartItem item);

        void RemoveFromCart(int cartItemId);

        void ClearCart(int userId);

        void ClearWishlist(int userId);

        void SetPaymentMethodOnCart(int userId, string paymentMethodName, string paymentMethodData);

        void SetShippingMethodOnCart(int userId, string shippingMethodName);

        DiscountApplicationStatus SetDiscountCoupon(int userId, string discountCoupon);

        void SetAddresses(int userId, int billingAddressId, int shippingAddressId);
    }
}