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

namespace EvenCart.Services.Users
{
    public interface IUserRegistrationService
    {
        /// <summary>
        /// Tries to register a new user and returns if the registration succeeded for failed
        /// </summary>
        /// <returns></returns>
        UserRegistrationStatus Register(string email, string password, PasswordFormat passwordFormat);

        /// <summary>
        /// Tries to register a new user and returns if the registration succeded or failed
        /// </summary>
        /// <param name="user"></param>
        /// <param name="passwordFormat"></param>
        /// <returns></returns>
        UserRegistrationStatus Register(User user, PasswordFormat passwordFormat);

        /// <summary>
        /// Updates the user's password
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="password"></param>
        /// <param name="passwordFormat"></param>
        void UpdatePassword(int userId, string password, PasswordFormat passwordFormat);
    }
}