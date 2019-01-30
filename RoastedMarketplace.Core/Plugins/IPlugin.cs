using System.Collections.Generic;
using DotEntity.Versioning;

namespace RoastedMarketplace.Core.Plugins
{
    public interface IPlugin
    {
        /// <summary>
        /// Override this method to include operations that you wish to be perform on module installation
        /// </summary>
        void Install();

        /// <summary>
        /// Override this method to include operations that you wish to be perform on module uninstallation
        /// </summary>
        void Uninstall();

        /// <summary>
        /// Gets the database versions of the plugin
        /// </summary>
        IList<IDatabaseVersion> GetDatabaseVersions();

        /// <summary>
        /// The info about the plugin
        /// </summary>
        PluginInfo PluginInfo { get; set; }

        /// <summary>
        /// Gets or sets the configuration url for the plugin
        /// </summary>
        string ConfigurationUrl { get; }
    }
}