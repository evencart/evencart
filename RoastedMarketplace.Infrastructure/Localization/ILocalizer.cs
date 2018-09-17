namespace RoastedMarketplace.Infrastructure.Localization
{
    public interface ILocalizer
    {
        string Localize(string key, string languageCode = "en-US");

        void LoadLanguage(string languageCode);
    }
}