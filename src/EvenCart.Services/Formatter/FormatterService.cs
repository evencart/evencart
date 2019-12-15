using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using EvenCart.Data.Extensions;
using EvenCart.Services.Serializers;

namespace EvenCart.Services.Formatter
{
    public class FormatterService : IFormatterService
    {
        private readonly IDataSerializer _dataSerializer;
        private static IDictionary<string, CultureInfo> _cultureInfos;
        public FormatterService(IDataSerializer dataSerializer)
        {
            _dataSerializer = dataSerializer;
            _cultureInfos = new Dictionary<string, CultureInfo>(StringComparer.InvariantCultureIgnoreCase);
        }


        public string FormatCurrency(decimal amount, string languageCultureCode, bool includeSymbol = true, string customFormat = null)
        {
            var culture = new CultureInfo(languageCultureCode);
            var format = customFormat.IsNullEmptyOrWhiteSpace() ? "C" : customFormat;
            return amount.ToString(format, culture);
        }

        public string FormatCurrencyFromIsoCode(decimal amount, string isoCode, bool includeSymbol = true, string customFormat = null)
        {
            if (!_cultureInfos.TryGetValue(isoCode, out var culture))
            {
                culture = CultureInfo.GetCultures(CultureTypes.AllCultures).Where(x => !x.IsNeutralCulture).FirstOrDefault(x =>
                {
                    try
                    {
                        var region = new RegionInfo(x.LCID);
                        return string.Equals(region.ISOCurrencySymbol, isoCode, StringComparison.InvariantCultureIgnoreCase);
                    }
                    catch
                    {
                        return false;
                    }
                });
                if (culture != null)
                    _cultureInfos.TryAdd(isoCode, culture);
            }

            var format = customFormat.IsNullEmptyOrWhiteSpace() ? "C" : customFormat;
            if (culture != null)
                return amount.ToString(format, culture);
            return amount.ToString(format, CultureInfo.InvariantCulture);
        }

        public string FormatDateTime(DateTime dateTime, string languageCultureCode, bool onlyDate = false)
        {
            var culture = new CultureInfo(languageCultureCode);
            return onlyDate ? dateTime.ToString(culture.DateTimeFormat.ShortDatePattern) : dateTime.ToString(culture);
        }

        public string FormatProductAttributes(string attributeJson)
        {
            if (string.IsNullOrEmpty(attributeJson))
                return string.Empty;
            var productAttributes =
                _dataSerializer.DeserializeAs<Dictionary<string, IList<string>>>(attributeJson);
            var attributeText = string.Join(Environment.NewLine, productAttributes.Select(y => y.Key + " : " + string.Join(", ", y.Value)));
            return attributeText;
        }
    }
}