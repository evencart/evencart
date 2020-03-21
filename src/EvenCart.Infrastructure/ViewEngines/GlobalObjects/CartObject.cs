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
using System.Linq;
using EvenCart.Core.Data;
using EvenCart.Core.Infrastructure;
using EvenCart.Data.Entity.Cultures;
using EvenCart.Data.Entity.Purchases;
using EvenCart.Data.Entity.Settings;
using EvenCart.Data.Extensions;
using EvenCart.Services.Formatter;
using EvenCart.Services.Helpers;
using EvenCart.Services.Products;
using EvenCart.Services.Purchases;
using EvenCart.Infrastructure.MediaServices;
using EvenCart.Infrastructure.ViewEngines.GlobalObjects.Implementations;

namespace EvenCart.Infrastructure.ViewEngines.GlobalObjects
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
            var dataSerializer = DependencyResolver.Resolve<IDataSerializer>();

            if (ApplicationEngine.CurrentUser == null)
                return null;
            var cart = _isWishList
                ? cartService.GetWishlist(ApplicationEngine.CurrentUser.Id)
                : cartService.GetCart(ApplicationEngine.CurrentUser.Id);
            

            var conflictingProducts = CartHelper.HasConflictingProducts(cart);
            if(!conflictingProducts)
                //refresh the cart items if necessary
                CartHelper.RefreshCart(cart);

            var selectedShippingOption = "";
            if (!cart.SelectedShippingOption.IsNullEmptyOrWhiteSpace())
            {
                var selectionOptions = dataSerializer.DeserializeAs<IList<ShippingOption>>(cart.SelectedShippingOption);
                selectedShippingOption = string.Join(", ", selectionOptions.Select(x => x.Name));
            }
            var cartModel = new CartImplementation() {
                Items = new List<CartItemImplementation>(),
                TotalItems = cart.CartItems.Sum(x => x.Quantity),
                DiscountCoupon = cart.DiscountCoupon?.HasCouponCode ?? false ? cart.DiscountCoupon?.CouponCode : "",
                ShippingMethodName = cart.ShippingMethodDisplayName,
                ShippingMethodFee = cart.ShippingFee,
                ShippingOptionName = selectedShippingOption,
                ConflictingProducts = conflictingProducts
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
                        AttributeText = formatterService.FormatProductAttributes(x.AttributeJson),
                        ProductSaleType = x.Product.ProductSaleType,
                        SubscriptionCycle = x.Product.SubscriptionCycle,
                        CycleCount = x.Product.CycleCount
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
            cartModel.FinalAmount = cartModel.ShippingMethodFee + cartModel.SubTotal + cartModel.Tax - cartModel.Discount;

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