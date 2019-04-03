using System;
using DotLiquid;
using RoastedMarketplace.Core.Infrastructure;
using RoastedMarketplace.Infrastructure.Localization;
using RoastedMarketplace.Services.Serializers;

namespace RoastedMarketplace.Infrastructure.ViewEngines.Filters
{
    public static class TextFilters
    {
        public static string T(string input)
        {
            var localizer = DependencyResolver.Resolve<ILocalizer>();
            return localizer.Localize(input);
        }

        public static string Pluralize(Context context, int input, string singular, string plural)
        {
            return string.Concat($"{input} ", T(input == 1 ? singular : plural));
        }

        public static string WithCurrency(Context context, decimal input)
        {
            return input.ToCurrency();
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
    }
}