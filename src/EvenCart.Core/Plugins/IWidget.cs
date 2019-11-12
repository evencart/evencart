using System;
using System.Collections.Generic;

namespace EvenCart.Core.Plugins
{
    /// <summary>
    /// Interface for creating a widget module
    /// </summary>
    public interface IWidget
    {
        string DisplayName { get; }

        string SystemName { get; }

        IList<string> WidgetZones { get; }

        bool HasConfiguration { get; }

        bool SkipDragging { get; }

        string ConfigurationUrl { get; }

        Type SettingsType { get; }

        object GetViewObject(object settings);
    }
}