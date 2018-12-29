using System.Collections.Generic;
using RoastedMarketplace.Core.Plugins;

namespace RoastedMarketplace.Services.Plugins
{
    /// <summary>
    /// Interface for creating a widget module
    /// </summary>
    public interface IComponentPlugin : IPlugin
    {
        IList<string> GetComponentLocations();

        string ComponentName { get; }
    }
}