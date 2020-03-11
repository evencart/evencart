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

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using EvenCart.Data.Entity.Cultures;
using EvenCart.Services.HttpServices;

namespace EvenCart.Services.Cultures
{
    public class DefaultCurrencyRateProvider : ICurrencyRateProvider
    {
        private const string FetchUrl = "https://www.ecb.europa.eu/stats/eurofxref/eurofxref-daily.xml";

        private readonly IRequestProvider _requestProvider;
        public DefaultCurrencyRateProvider(IRequestProvider requestProvider)
        {
            _requestProvider = requestProvider;
        }

        public async Task<IList<CurrencyRate>> GetLatestRates(string baseCurrencyCode)
        {
            var responseXml = await _requestProvider.GetStringAsync(FetchUrl);
            //let's parse the response xml
            var listRates = new List<CurrencyRate>();
            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(responseXml);
            var cubeNode = xmlDocument.DocumentElement.SelectSingleNode("/*[local-name()='Envelope']/*[local-name()='Cube']/*[local-name()='Cube']");
            foreach (XmlNode childNode in cubeNode.ChildNodes)
            {
                var isoCode = childNode.Attributes["currency"].Value;
                var rate = decimal.Parse(childNode.Attributes["rate"].Value);
                listRates.Add(new CurrencyRate(isoCode, rate));
            }

            //by default the base used by ecb web service is EUR, so we'll now have to convert them to target type
            if (baseCurrencyCode == "EUR")
                return listRates;
            else
            {
                //convert the rates now
                var baseRateCurrency = listRates.FirstOrDefault(x => x.IsoCode == baseCurrencyCode);
                if (baseRateCurrency == null)
                    return listRates;
                var baseRate = baseRateCurrency.Rate;
                baseRateCurrency.Rate = 1;
                foreach (var cRate in listRates)
                {
                    cRate.Rate = cRate.Rate / baseRate;
                }
            }
            return listRates;
        }

        public string ProviderName => "Default Provider";

        public string ProviderSystemName => "EvenCart.Services.Cultures.DefaultCurrencyRateProvider";
    }
}