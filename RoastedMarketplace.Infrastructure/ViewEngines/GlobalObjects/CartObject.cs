using System.Collections.Generic;
using System.Linq;
using RoastedMarketplace.Core.Infrastructure;
using RoastedMarketplace.Data.Entity.Settings;
using RoastedMarketplace.Infrastructure.MediaServices;
using RoastedMarketplace.Infrastructure.ViewEngines.GlobalObjects.Implementations;
using RoastedMarketplace.Services.Formatter;
using RoastedMarketplace.Services.Helpers;
using RoastedMarketplace.Services.Purchases;

namespace RoastedMarketplace.Infrastructure.ViewEngines.GlobalObjects
{
    public class CartObject : GlobalObject
    {
        public override object GetObject()
        {
            var cartService = DependencyResolver.Resolve<ICartService>();
            var mediaAccountant = DependencyResolver.Resolve<IMediaAccountant>();
            var formatterService = DependencyResolver.Resolve<IFormatterService>();
            var taxSettings = DependencyResolver.Resolve<TaxSettings>();
           
            if (ApplicationEngine.CurrentUser == null)
                return null;
            var cart = cartService.GetCart(ApplicationEngine.CurrentUser.Id);
            //refresh the cart items if necessary
            CartHelper.RefreshCart(cart);

            var cartModel = new CartImplementation() {
                Items = new List<CartItemImplementation>(),
                TotalItems = cart.CartItems.Sum(x => x.Quantity),
                DiscountCoupon = cart.DiscountCoupon?.HasCouponCode ?? false ? cart.DiscountCoupon?.CouponCode : ""
            };

            cartModel.Items = cart.CartItems.Select(x =>
                {

                    var cartItem = new CartItemImplementation {
                        Id = x.Id,
                        ProductId = x.ProductId,
                        ProductName = x.Product.Name,
                        Price = decimal.Round(taxSettings.DisplayProductPricesWithoutTax ? x.Price : x.Price + x.Tax, 2),
                        Quantity = x.Quantity,
                        Discount = x.Discount,
                        ComparePrice = x.ComparePrice,
                        Tax = x.Tax,
                        TaxPercent = x.TaxPercent,
                        ImageUrl = mediaAccountant.GetPictureUrl(x.Product.MediaItems.FirstOrDefault(), ApplicationEngine.ActiveTheme.CartItemImageSize, true),
                        Slug = x.Product.SeoMeta.Slug,
                        AttributeText = formatterService.FormatProductAttributes(x.AttributeJson)
                    };
                    cartItem.SubTotal = cartItem.Price * cartItem.Quantity;
                    cartItem.FinalPrice = cartItem.SubTotal - cartItem.Tax - cartItem.Discount;
                    return cartItem;
                })
                .ToList();
            cartModel.SubTotal = cartModel.Items.Sum(x => x.SubTotal);
            cartModel.Tax = cartModel.Items.Sum(x => x.Tax);
            cartModel.CompareFinalAmount = cartModel.Items.Sum(x => x.ComparePrice ?? 0);
            cartModel.Discount = cart.Discount;
            if (cartModel.Discount == 0)
                cartModel.Discount = cartModel.Items.Sum(x => x.Discount);
            cartModel.FinalAmount = cartModel.SubTotal - cartModel.Discount;
            if (taxSettings.DisplayProductPricesWithoutTax)
                cartModel.FinalAmount += cartModel.Tax;
            return cartModel;
        }
    }
}