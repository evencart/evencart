using System.Collections.Generic;

namespace EvenCart.Infrastructure.Theming
{
    public interface IThemeProvider
    {
        ThemeInfo GetActiveTheme();

        string GetThemePath(string themeName);

        IList<ThemeInfo> GetAvailableThemes();

        void ResetActiveTheme();
    }
}