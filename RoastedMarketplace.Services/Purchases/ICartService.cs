using RoastedMarketplace.Core.Services;
using RoastedMarketplace.Data.Entity.Purchases;
using RoastedMarketplace.Services.Promotions;

namespace RoastedMarketplace.Services.Purchases
{
    public interface ICartService : IFoundationEntityService<Cart>
    {
        Cart GetCart(int userId);

        Cart GetWishlist(int userId);

        void AddToCart(int userId, CartItem item, Transaction transaction = null);

        void UpdateCart(int userId, CartItem item, Transaction transaction = null);

        void AddToWishlist(int userId, CartItem item, Transaction transaction = null);

        void RemoveFromCart(int cartItemId, Transaction transaction = null);

        void ClearCart(int userId);

        void ClearWishlist(int userId);

        void SetPaymentMethodOnCart(int userId, string paymentMethodName, string paymentMethodData, Cart cart = null);

        void SetShippingMethodOnCart(int userId, string shippingMethodName, Cart cart = null);

        DiscountApplicationStatus SetDiscountCoupon(int userId, string discountCoupon, Cart cart = null);

        void ClearDiscountCoupon(int userId, Cart cart = null);

        void SetAddresses(int userId, int billingAddressId, int shippingAddressId);
    }
}