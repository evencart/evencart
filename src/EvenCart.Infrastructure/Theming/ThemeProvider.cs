using EvenCart.Core.Infrastructure.Providers;
using EvenCart.Services.Serializers;

namespace EvenCart.Infrastructure.Theming
{
    public class ThemeProvider : IThemeProvider
    {
        private readonly ILocalFileProvider _localFileProvider;
        private readonly IDataSerializer _dataSerializer;
        public ThemeProvider(ILocalFileProvider localFileProvider, IDataSerializer dataSerializer)
        {
            _localFileProvider = localFileProvider;
            _dataSerializer = dataSerializer;
        }

        private ThemeInfo _cachedThemeInfo = null;
        public ThemeInfo GetActiveTheme()
        {
            if (_cachedThemeInfo != null)
                return _cachedThemeInfo;
            var themeName = "Default";

            var themeConfigPath = _localFileProvider.CombinePaths(GetThemePath(themeName), "config.json");
            var configJson = _localFileProvider.ReadAllText(themeConfigPath);
            _cachedThemeInfo = _dataSerializer.DeserializeAs<ThemeInfo>(configJson);
            return _cachedThemeInfo;
        }

        public string GetThemePath(string themeName)
        {
            return ApplicationEngine.MapPath($"~/Content/Themes/{themeName}");
        }
    }
}