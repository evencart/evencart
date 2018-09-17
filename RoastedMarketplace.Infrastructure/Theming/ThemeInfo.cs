namespace RoastedMarketplace.Infrastructure.Theming
{
    public class ThemeInfo
    {
        public string ThemeName { get; set; }

        public string GetThemePath()
        {
            return ApplicationEngine.MapPath($"~/Content/Themes/{ThemeName}");
        }
    }
}