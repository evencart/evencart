using System.Collections.Generic;
using System.IO;
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
    }
}