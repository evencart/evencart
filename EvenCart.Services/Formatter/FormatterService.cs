using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using EvenCart.Services.Serializers;

namespace EvenCart.Services.Formatter
{
    public class FormatterService : IFormatterService
    {
        private readonly IDataSerializer _dataSerializer;

        public FormatterService(IDataSerializer dataSerializer)
        {
            _dataSerializer = dataSerializer;
        }


        public string FormatCurrency(decimal amount, string languageCultureCode, bool includeSymbol = true)
        {
            var culture = new CultureInfo(languageCultureCode);
            return amount.ToString("C", culture);
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