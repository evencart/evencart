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

namespace EvenCart.Infrastructure.Mvc
{
    public static class ErrorCodes
    {
        public const string ParentEntityMustBeNonZero = "PARENT_ID_MUST_NONZERO";

        public const string CaptchaValidationRequired = "CAPTCHA_VALIDATION_REQUIRED";

        public const string AntiForgeryValidationFailed = "ANTIFORGERY_VALIDATION_FAILED";

        public const string RequiresAuthenticatedUser = "REQUIRES_USER_AUTHENTICATION";
    }
}