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
using EvenCart.Data.Extensions;
using EvenCart.Services.Formatter;

namespace EvenCart.Infrastructure.Extensions
{
    public static class FormatterServiceExtensions
    {
        public static string FormatCurrency(this IFormatterService formatterService, decimal amount, bool includeSymbol = true)
        {
            if (ApplicationEngine.IsAdmin())
                return formatterService.FormatCurrency(amount, ApplicationEngine.BaseCurrency.CultureCode,
                    includeSymbol, ApplicationEngine.BaseCurrency.CustomFormat);
            return formatterService.FormatCurrency(amount, ApplicationEngine.CurrentCurrency.CultureCode, includeSymbol, ApplicationEngine.BaseCurrency.CustomFormat);
        }

        public static string ToCurrency(this decimal amount)
        {
            var formatterService = DependencyResolver.Resolve<IFormatterService>();
            return formatterService.FormatCurrency(amount);
        }

        public static string ToCurrency(this decimal amount, string code)
        {
            if (code.IsNullEmptyOrWhiteSpace())
                return amount.ToCurrency();
            var formatterService = DependencyResolver.Resolve<IFormatterService>();
            return formatterService.FormatCurrencyFromIsoCode(amount, code);
        }

        public static string ToCurrency(this decimal? amount)
        {
            return ToCurrency(amount ?? 0);
        }
    }
}