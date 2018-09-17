using System.Collections.Generic;
using RoastedMarketplace.Core.Modules;

namespace RoastedMarketplace.Services.Modules
{
    /// <summary>
    /// Interface for creating a widget module
    /// </summary>
    public interface IComponentModule : IModule
    {
        IList<string> GetComponentLocations();

        string ComponentName { get; }
    }
}