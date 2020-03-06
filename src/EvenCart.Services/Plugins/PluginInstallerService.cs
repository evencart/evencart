#region License
// Copyright (c) Sojatia Infocrafts Private Limited.
// The following code is part of EvenCart eCommerce Software (https://evencart.co) Dual Licensed under the terms of
// 
// 1. GNU GPLv3 with additional terms (available to read at https://evencart.co/license)
// 2. EvenCart Proprietary License (available to read at https://evencart.co/license/commercial-license).
// 
// You can select one of the above two licenses according to your requirements. The usage of this code is
// subject to the terms of the license chosen by you.
#endregion

using EvenCart.Core.Plugins;

namespace EvenCart.Services.Plugins
{
    public class PluginInstallerService : IPluginInstallerService
    {

        public void Install(PluginInfo pluginInfo)
        {
            if (pluginInfo.Installed)
                return;

            var moduleType = pluginInfo.LoadPluginInstance<IPlugin>();
            if(moduleType != null)
                moduleType.Install();

            pluginInfo.Installed = true;
        }

        public void Uninstall(PluginInfo pluginInfo)
        {
            if (!pluginInfo.Installed)
                return;

            var moduleType = pluginInfo.LoadPluginInstance<IPlugin>();
            if (moduleType != null)
                moduleType.Uninstall();
            pluginInfo.Installed = false;
        }
    }
}