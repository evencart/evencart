using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DotEntity;
using DotEntity.Enumerations;
using EvenCart.Core.Infrastructure;
using EvenCart.Core.Services;
using EvenCart.Data.Entity.Addresses;
using EvenCart.Data.Entity.MediaEntities;
using EvenCart.Data.Entity.Pages;
using EvenCart.Data.Entity.Promotions;
using EvenCart.Data.Entity.Purchases;
using EvenCart.Data.Entity.Shop;
using EvenCart.Data.Entity.Users;
using EvenCart.Data.Extensions;
using EvenCart.Services.Products;
using EvenCart.Services.Promotions;

namespace EvenCart.Services.Purchases
{
    public class CartService : FoundationEntityService<Cart>, ICartService
    {
        private readonly ICartItemService _cartItemService;
        public CartService(ICartItemService cartItemService)
        {
            _cartItemService = cartItemService;
        }

        public Cart GetCart(int userId)
        {
            var cart = GetCart(userId, false);
            cart.CartItems = cart.CartItems ?? new List<CartItem>();
            return cart;
        }

        public Cart GetWishlist(int userId)
        {
            var cart = GetCart(userId, true);
            cart.CartItems = cart.CartItems ?? new List<CartItem>();
            return cart;
        }

        private Cart GetCart(int userId, bool isWishlist)
        {
            Expression<Func<SeoMeta, bool>> seoMetaWhere = meta => meta.EntityName == "Product";
            Expression<Func<Product, bool>> productWhere = product => product.Published;
            Expression<Func<Address, bool>> addressWhere = address => address.EntityName == nameof(User);
            var userCart = Repository.Where(x => x.UserId == userId && x.IsWishlist == isWishlist)
                .Join<CartItem>("Id", "CartId", joinType: JoinType.LeftOuter)
                .Join<Product>("ProductId", "Id", joinType: JoinType.LeftOuter)
                .Join<SeoMeta>("Id", "EntityId", joinType: JoinType.LeftOuter)
                .Join<ProductMedia>("EntityId", "ProductId", joinType: JoinType.LeftOuter)
                .Join<Media>("MediaId", "Id", joinType: JoinType.LeftOuter)
                .Join<DiscountCoupon>("DiscountCouponId", "Id", SourceColumn.Parent, JoinType.LeftOuter)
                .Join<RestrictionValue>("Id", "DiscountCouponId", joinType: JoinType.LeftOuter)
                .Join<User>("UserId", "Id", SourceColumn.Parent, JoinType.LeftOuter)
                .Join<UserRole>("Id", "UserId", joinType: JoinType.LeftOuter)
                .Join<Role>("RoleId", "Id", joinType: JoinType.LeftOuter)
                .Join<Address>("BillingAddressId", "Id", SourceColumn.Parent, JoinType.LeftOuter)
                .Join<Country>("CountryId", "Id", joinType: JoinType.LeftOuter)
                .Join<Address>("ShippingAddressId", "Id", SourceColumn.Parent, JoinType.LeftOuter)
                .Join<Country>("CountryId", "Id", joinType: JoinType.LeftOuter)
                .Relate(RelationTypes.OneToMany<Cart, CartItem>())
                .Relate<Product>((cart, product) =>
                {
                    var cartItem = cart.CartItems.FirstOrDefault(x => x.ProductId == product.Id && x.Product == null);
                    if (cartItem != null)
                        cartItem.Product = product;
                })
                .Relate<SeoMeta>((cart, meta) =>
                {
                    var cartItem = cart.CartItems.FirstOrDefault(x => x.ProductId == meta.EntityId);
                    if (cartItem != null)
                        cartItem.Product.SeoMeta = meta;
                })
                .Relate<ProductMedia>((cart, media) =>
                {
                    var cartItem = cart.CartItems.FirstOrDefault(x => x.ProductId == media.ProductId);
                    if (cartItem?.Product != null)
                    {
                        //temporary storage for media ids
                        cartItem.Tag = cartItem.Tag ?? new List<int>();
                        cartItem.Product.MediaItems = cartItem.Product.MediaItems ?? new List<Media>();

                        (cartItem.Tag as List<int>).Add(media.MediaId);
                    }

                })
                .Relate<Media>((cart, media) =>
                {
                    var cartItem =
                        cart.CartItems.FirstOrDefault(
                            x => x.Tag != null && (x.Tag as List<int>).Contains(media.Id));
                    if (cartItem != null)
                    {
                        if (cartItem.Product.MediaItems.All(x => x.Id != media.Id))
                        {
                            cartItem.Product.MediaItems.Add(media);
                        }
                    }
                })
                .Relate(RelationTypes.OneToOne<Cart, DiscountCoupon>())
                .Relate<RestrictionValue>((cart, value) =>
                {
                    cart.DiscountCoupon.RestrictionValues =
                        cart.DiscountCoupon.RestrictionValues ?? new List<RestrictionValue>();
                    cart.DiscountCoupon.RestrictionValues.Add(value);
                })
                .Relate(RelationTypes.OneToOne<Cart, User>())
                .Relate<Role>((cart, role) =>
                {
                    cart.User.Roles = cart.User.Roles ?? new List<Role>();
                    cart.User.Roles.Add(role);
                })
                .Relate<Address>((cart, address) =>
                {
                    if (cart.BillingAddressId == address.Id)
                        cart.BillingAddress = address;
                    if (cart.ShippingAddressId == address.Id)
                        cart.ShippingAddress = address;
                })
                .Relate<Country>((cart, country) =>
                {
                    if (cart.BillingAddress?.CountryId == country.Id)
                        cart.BillingAddress.Country = country;
                    if (cart.ShippingAddress != null && cart.ShippingAddress?.CountryId == country.Id)
                        cart.ShippingAddress.Country = country;
                })
                .Where(seoMetaWhere)
                .Where(productWhere)
                .Where(addressWhere)
                .SelectNested()
                .FirstOrDefault();

            //do we have an empty cart
            userCart = userCart ?? Repository.Where(x => x.UserId == userId && x.IsWishlist == isWishlist).SelectSingle() ?? new Cart();

            if (userCart.Id == 0)
            {
                //save a new cart for user
                userCart.UserId = userId;
                userCart.IsWishlist = isWishlist;
                Insert(userCart);
            }
            return userCart;
        }

        public void AddToCart(int userId, CartItem item, Transaction transaction = null)
        {
            //get user's cart
            var userCart = GetCart(userId);
            //save the item now
            item.CartId = userCart.Id;
            _cartItemService.Insert(item, transaction);
        }

        public void UpdateCart(int userId, CartItem item, Transaction transaction = null)
        {
            _cartItemService.Update(item, transaction);
        }

        public void AddToWishlist(int userId, CartItem item, Transaction transaction = null)
        {
            //get user's cart
            var userCart = GetWishlist(userId);
            //save the item now
            item.CartId = userCart.Id;
            _cartItemService.Insert(item, transaction);
        }

        public void RemoveFromCart(int cartItemId, Transaction transaction = null)
        {
            _cartItemService.Delete(x => x.Id == cartItemId, transaction);
        }

        public void ClearCart(int userId)
        {
            //clear user's cart
            ClearCart(userId, false);
        }

        public void ClearWishlist(int userId)
        {
            //clear wishlist
            ClearCart(userId, true);
        }

        public void SetPaymentMethodOnCart(int userId, string paymentMethodName, string paymentMethodData, Cart cart = null)
        {
            cart = cart ?? GetCart(userId);
            cart.PaymentMethodName = paymentMethodName;
            cart.PaymentMethodData = paymentMethodData;
            Update(cart);
        }

        public void SetShippingMethodOnCart(int userId, string shippingMethodName, Cart cart = null)
        {
            cart = cart ?? GetCart(userId);
            cart.ShippingMethodName = shippingMethodName;
            Update(cart);
        }

        public DiscountApplicationStatus SetDiscountCoupon(int userId, string discountCoupon, Cart cart = null)
        {
            cart = cart ?? GetCart(userId);
            var priceAccountant = DependencyResolver.Resolve<IPriceAccountant>();
            return priceAccountant.ApplyDiscountCoupon(discountCoupon, cart);
        }

        public void ClearDiscountCoupon(int userId, Cart cart = null)
        {
            cart = cart ?? GetCart(userId);
            var priceAccountant = DependencyResolver.Resolve<IPriceAccountant>();
            priceAccountant.ClearCouponCode(cart);
        }

        public void SetAddresses(int userId, int billingAddressId, int shippingAddressId)
        {
            var cart = GetCart(userId);
            cart.BillingAddressId = billingAddressId;
            cart.ShippingAddressId = shippingAddressId;
            Update(cart);
        }

        private void ClearCart(int userId, bool isWishlist)
        {
            var cart = isWishlist ? GetWishlist(userId) : GetCart(userId);
            _cartItemService.Delete(x => x.CartId == cart.Id);
            cart.BillingAddressId = 0;
            cart.ShippingAddressId = 0;
            cart.DiscountCouponId = 0;
            cart.PaymentMethodName = null;
            cart.PaymentMethodData = null;
            cart.PaymentMethodFee = 0;
            cart.CompareFinalAmount = 0;
            cart.FinalAmount = 0;
            cart.ShippingFee = 0;
            cart.Discount = 0;
            cart.DiscountCouponId = 0;
            Update(cart);
        }
    }
}