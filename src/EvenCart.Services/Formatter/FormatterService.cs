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
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using EvenCart.Core.Data;
using EvenCart.Data.Extensions;

namespace EvenCart.Services.Formatter
{
    public class FormatterService : IFormatterService
    {
        private readonly IDataSerializer _dataSerializer;
        private static IDictionary<string, CultureInfo> _cultureInfos;
        public FormatterService(IDataSerializer dataSerializer)
        {
            _dataSerializer = dataSerializer;
            _cultureInfos = new ConcurrentDictionary<string, CultureInfo>(StringComparer.InvariantCultureIgnoreCase);
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
                {
                    try
                    {
                        _cultureInfos.TryAdd(isoCode, culture);
                    }
                    catch
                    {
                        //other thread might have added
                        //todo: keep checking if this is indeed the issue.
                    }
                }
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