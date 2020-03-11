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

namespace EvenCart.Data.Entity.Users
{
    public enum UserCodeType
    {
        /// <summary>
        /// Specifies a code for resetting password
        /// </summary>
        PasswordReset,
        /// <summary>
        /// Specifies a code for email verification
        /// </summary>
        EmailVerification,
        /// <summary>
        /// Specifies a code for registration by invitation
        /// </summary>
        RegistrationInvitation,
        /// <summary>
        /// Specifies a 6 digit otp for email verification
        /// </summary>
        EmailOtp
    }
}