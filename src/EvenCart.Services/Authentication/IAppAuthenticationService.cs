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

using EvenCart.Data.Entity.Users;
using EvenCart.Data.Enum;

namespace EvenCart.Services.Authentication
{
    public interface IAppAuthenticationService
    {
        /// <summary>
        /// Signs in the user
        /// </summary>
        LoginStatus SignIn(string email, string name = "", bool isPersistent = false,  bool forceCreateNewAccount = false);

        /// <summary>
        /// Signs in the user
        /// </summary>
        LoginStatus SignIn(string authenticationScheme, string email, string name = "", bool isPersistent = false, bool forceCreateNewAccount = false);

        /// <summary>
        /// Signs in the user in imitation mode
        /// </summary>
        LoginStatus ImitationModeSignIn(string targetUserEmail, string imitatorEmail);

        /// <summary>
        /// Signs out the current logged in user
        /// </summary>
        void SignOut();

        /// <summary>
        /// Signs in the visitor as guest
        /// </summary>
        /// <returns></returns>
        LoginStatus GuestSignIn();
      

        /// <summary>
        /// Gets the current user
        /// </summary>
        /// <returns></returns>
        User GetCurrentUser();
    }
}