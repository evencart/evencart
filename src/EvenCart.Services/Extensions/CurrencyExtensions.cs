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

using System.Globalization;
using EvenCart.Data.Entity.Cultures;

namespace EvenCart.Services.Extensions
{
    public static class CurrencyExtensions
    {
        public static string GetSymbol(this Currency currency)
        {
            var culture = new CultureInfo(currency.CultureCode);
            var regionInfo = new RegionInfo(culture.LCID);
            return regionInfo.CurrencySymbol;
        }
    }
}