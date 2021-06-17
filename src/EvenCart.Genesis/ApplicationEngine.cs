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

using System.Linq;
using EvenCart.Data.Entity.Purchases;
using EvenCart.Services.Orders;
using Genesis;
using Genesis.Modules.Users;
using Genesis.Services;

namespace EvenCart.Genesis
{
    public class ApplicationEngine : GenesisEngine
    {
        public override IApplicationConfig StaticConfig => GenesisApp.Current.ApplicationConfig;

        public override LoginStatus SignIn(string email, string name, bool rememberMe, bool tokenAuth = false)
        {
            var isVisitor = CurrentUser != null && CurrentUser.IsVisitor();
            var visitorId = isVisitor ? CurrentUser.Id : 0;

            var status = base.SignIn(email, name, rememberMe, tokenAuth);
            //if we are here, the login was successful. If already logged in user was a visitor, we'll move the cart items 
            //from old user to new user
            if (status == LoginStatus.Success && isVisitor)
            {
                //move all the cart items of guest user to logged in user
                var cartService = D.Resolve<ICartService>();
                var cartItemService = D.Resolve<ICartItemService>();
                var visitorCart = cartService.GetCart(visitorId);
                if (visitorCart.CartItems.Any())
                {
                    Transaction.Initiate(transaction =>
                    {
                        //move the cart items to the current user's cart
                        var registeredUserCart = cartService.GetCart(CurrentUser.Id);
                        foreach (var cItem in visitorCart.CartItems)
                        {
                            cItem.CartId = registeredUserCart.Id;
                            CartItem sameCartItem = null;
                            if ((sameCartItem = registeredUserCart.CartItems.FirstOrDefault(x =>
                                    x.ProductId == cItem.ProductId && x.AttributeJson == cItem.AttributeJson)) != null)
                            {
                                sameCartItem.Quantity++;
                                cartItemService.Update(sameCartItem, transaction);
                                cartItemService.Delete(cItem, transaction);
                            }
                            else
                            {
                                cartItemService.Update(cItem, transaction);
                            }

                        }
                    });
                }
            }

            return status;
        }
    }
}