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