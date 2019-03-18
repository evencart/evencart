using RoastedMarketplace.Infrastructure.Mvc.Models;

namespace RoastedMarketplace.Areas.Administration.Models.Settings
{
    public class LocalizationSettingsModel : SettingsModel
    {
        public bool AllowUserToSelectCurrency { get; set; }

        public bool AllowUserToSelectLanguage { get; set; }

        public int BaseCurrencyId { get; set; }

        public int PrimaryCurrencyId { get; set; }

        public string DefaultLanguage { get; set; }
    }
}