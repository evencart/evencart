using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using RoastedMarketplace.Services.Serializers;

namespace RoastedMarketplace.Services.Formatter
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

        public string FormatProductAttributes(string attributeJson)
        {
            var productAttributes =
                _dataSerializer.DeserializeAs<Dictionary<string, IList<string>>>(attributeJson);
            var attributeText = string.Join(Environment.NewLine, productAttributes.Select(y => y.Key + " : " + string.Join(", ", y.Value)));
            return attributeText;
        }
    }
}