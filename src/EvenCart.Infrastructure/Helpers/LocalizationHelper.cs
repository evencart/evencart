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

using EvenCart.Core.Infrastructure;
using EvenCart.Infrastructure.Localization;

namespace EvenCart.Infrastructure.Helpers
{
    public static class LocalizationHelper
    {
        public static string Localize(string resource, params object[] arguments)
        {
            return Localize(resource, ApplicationEngine.CurrentLanguageCultureCode, arguments);
        }

        public static string Localize(string resource, string languageCultureCode, params object[] arguments)
        {
            if (resource == null)
                return null;
            var localizer = DependencyResolver.Resolve<ILocalizer>();
            var localized = localizer.Localize(resource, languageCultureCode);
            return string.Format(localized, arguments);
        }
    }
}