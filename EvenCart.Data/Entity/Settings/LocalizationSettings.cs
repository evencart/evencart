using EvenCart.Core.Config;

namespace EvenCart.Data.Entity.Settings
{
    public class LocalizationSettings : ISettingGroup
    {
        public bool AllowUserToSelectCurrency { get; set; }

        public bool AllowUserToSelectLanguage { get; set; }

        public int BaseCurrencyId { get; set; }

        public int PrimaryCurrencyId { get; set; }

        public string DefaultLanguage { get; set; }
    }
}