using System.Collections.Generic;

namespace EvenCart.Infrastructure.Theming
{
    public interface IThemeProvider
    {
        ThemeInfo GetActiveTheme();

        string GetThemePath(string themeName);

        IList<ThemeInfo> GetAvailableThemes();

        ThemeInfo LoadTheme(string directoryPath, bool pendingRestart = false);

        void ResetActiveTheme();
    }
}