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

using System;
using DotLiquid;
using Genesis;
using Genesis.Data;
using Genesis.Infrastructure;
using EvenCart.Data.Entity.Settings;
using EvenCart.Data.Helpers;
using EvenCart.Infrastructure.Extensions;
using EvenCart.Infrastructure.Localization;
using EvenCart.Services.Helpers;

namespace EvenCart.Infrastructure.ViewEngines.Filters
{
    public static class TextFilters
    {
        public static string T(string input)
        {
            var localizer = DependencyResolver.Resolve<ILocalizer>();
            return localizer.Localize(input, ApplicationEngine.CurrentLanguage.CultureCode);
        }

        public static string Pluralize(Context context, int input, string singular, string plural)
        {
            return string.Concat($"{input} ", T(input == 1 ? singular : plural));
        }

        public static string WithCurrency(Context context, decimal input, string currency = null)
        {
            return currency == null ? input.ToCurrency() : input.ToCurrency(currency);
        }

        public static string NewLine2Br(string input)
        {
            return input.Replace(Environment.NewLine, "<br/>");
        }

        public static string ScriptJson(Context context, object input, string variableName)
        {
            var serializer = DependencyResolver.Resolve<IDataSerializer>();
            var json = serializer.Serialize(input);
            return $"<script type='text/javascript'>var {variableName}={json};</script>";
        }

        public static string AbsoluteUrl(string input)
        {
            var generalSettings = DependencyResolver.Resolve<GeneralSettings>();
            return WebHelper.GetUrlFromPath(input, generalSettings.StoreDomain);
        }

        public static string StripHtml(string input)
        {
            return HtmlUtility.StripHtml(input);
        }

        public static string Pretty(DateTime date)
        {
            return DateTimeHelper.GetRelativeDate(date);
        }
    }
}