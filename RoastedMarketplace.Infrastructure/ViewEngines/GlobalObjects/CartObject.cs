using System.Collections.Generic;
using System.Linq;
using RoastedMarketplace.Core.Infrastructure;
using RoastedMarketplace.Data.Entity.Cultures;
using RoastedMarketplace.Data.Entity.Settings;
using RoastedMarketplace.Infrastructure.MediaServices;
using RoastedMarketplace.Infrastructure.ViewEngines.GlobalObjects.Implementations;
using RoastedMarketplace.Services.Formatter;
using RoastedMarketplace.Services.Helpers;
using RoastedMarketplace.Services.Products;
using RoastedMarketplace.Services.Purchases;

namespace RoastedMarketplace.Infrastructure.ViewEngines.GlobalObjects
{
    public class CartObject : GlobalObject
    {
        private bool _isWishList;
        public CartObject(bool isWishList)
        {
            _isWishList = isWishList;
        }

        public CartObject() : this(false) { }

        public override object GetObject()
        {
            var cartService = DependencyResolver.Resolve<ICartService>();
            var mediaAccountant = DependencyResolver.Resolve<IMediaAccountant>();
            var formatterService = DependencyResolver.Resolve<IFormatterService>();
            var taxSettings = DependencyResolver.Resolve<TaxSettings>();
            var priceAccountant = DependencyResolver.Resolve<IPriceAccountant>();

            if (ApplicationEngine.CurrentUser == null)
                return null;
            var cart = _isWishList
                ? cartService.GetWishlist(ApplicationEngine.CurrentUser.Id)
                : cartService.GetCart(ApplicationEngine.CurrentUser.Id);

            //refresh the cart items if necessary
            CartHelper.RefreshCart(cart);

            var cartModel = new CartImplementation() {
                Items = new List<CartItemImplementation>(),
                TotalItems = cart.CartItems.Sum(x => x.Quantity),
                DiscountCoupon = cart.DiscountCoupon?.HasCouponCode ?? false ? cart.DiscountCoupon?.CouponCode : ""
            };
            var currentCurrency = ApplicationEngine.CurrentCurrency;
            cartModel.Items = cart.CartItems.Select(x =>
                {

                    var cartItem = new CartItemImplementation {
                        Id = x.Id,
                        ProductId = x.ProductId,
                        ProductName = x.Product.Name,
                        Price = taxSettings.DisplayProductPricesWithoutTax ? x.Price : x.Price + x.Tax,
                        Quantity = x.Quantity,
                        Discount = x.Discount,
                        ComparePrice = x.ComparePrice,
                        Tax = x.Tax,
                        TaxPercent = x.TaxPercent,
                        ImageUrl = mediaAccountant.GetPictureUrl(x.Product.MediaItems?.FirstOrDefault(), ApplicationEngine.ActiveTheme.CartItemImageSize, true),
                        Slug = x.Product.SeoMeta.Slug,
                        AttributeText = formatterService.FormatProductAttributes(x.AttributeJson)
                    };
                    cartItem.SubTotal = cartItem.Price * cartItem.Quantity;
                    cartItem.FinalPrice = cartItem.SubTotal + cartItem.Tax - cartItem.Discount;

                    //currency convert if required
                    cartItem.Price = priceAccountant.ConvertCurrency(cartItem.Price, currentCurrency, Rounding.Default);
                    cartItem.Tax = priceAccountant.ConvertCurrency(cartItem.Tax, currentCurrency, Rounding.Default);
                    cartItem.Discount = priceAccountant.ConvertCurrency(cartItem.Discount, currentCurrency, Rounding.Default);
                    cartItem.SubTotal = priceAccountant.ConvertCurrency(cartItem.SubTotal, currentCurrency, Rounding.Default);
                    cartItem.FinalPrice = priceAccountant.ConvertCurrency(cartItem.FinalPrice, currentCurrency, Rounding.Default);
                    cartItem.ComparePrice = priceAccountant.ConvertCurrency(cartItem.ComparePrice ?? 0, currentCurrency, Rounding.Default);
                    return cartItem;
                })
                .ToList();
            cartModel.SubTotal = cart.CartItems.Sum(x => x.Price * x.Quantity);
            cartModel.Tax = cart.CartItems.Sum(x => x.Tax);
            cartModel.CompareFinalAmount = cart.CartItems.Sum(x => x.ComparePrice ?? 0);
            cartModel.Discount = cart.Discount + cart.CartItems.Sum(x => x.Discount);
            cartModel.FinalAmount = cartModel.SubTotal + cartModel.Tax - cartModel.Discount;

            //convert currency
            cartModel.SubTotal = priceAccountant.ConvertCurrency(cartModel.SubTotal, currentCurrency, Rounding.Default);
            cartModel.Tax = priceAccountant.ConvertCurrency(cartModel.Tax, currentCurrency, Rounding.Default);
            cartModel.CompareFinalAmount = priceAccountant.ConvertCurrency(cartModel.CompareFinalAmount, currentCurrency, Rounding.Default);
            cartModel.Discount = priceAccountant.ConvertCurrency(cartModel.Discount, currentCurrency, Rounding.Default);
            cartModel.FinalAmount = priceAccountant.ConvertCurrency(cartModel.FinalAmount, currentCurrency);

            return cartModel;
        }

        public override bool RenderInAdmin => false;
        public override bool RenderInPublic => true;
    }
}