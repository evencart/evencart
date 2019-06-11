using System.Collections.Generic;
using System.Linq;

namespace Shipping.UPS
{
    public static class UPSProviderConfig
    {
        public const string UPSProviderSettingsRouteName = "UPSProviderSettings";

        public static Dictionary<string, string> AvailableServices = new Dictionary<string, string>
        {
            {"UPS Next Day Air", "01"},
            {"UPS 2nd Day Air", "02"},
            {"UPS Ground", "03"},
            {"UPS Worldwide Express", "07"},
            {"UPS Worldwide Expedited", "08"},
            {"UPS Standard", "11"},
            {"UPS 3 Day Select", "12"},
            {"UPS Next Day Air Saver", "13"},
            {"UPS Next Day Air Early A.M.", "14"},
            {"UPS Worldwide Express Plus", "54"},
            {"UPS 2nd Day Air A.M.", "59"},
            {"UPS Saver", "65"},
            {"UPS Today Standard", "82"},
            {"UPS Today Dedicated Courier", "83"},
            {"UPS Today Express", "85"},
            {"UPS Today Express Saver", "86"}
        };

        public static string GetServiceName(string code)
        {
            return AvailableServices.FirstOrDefault(x => x.Value == code).Key;
        }
    }
}