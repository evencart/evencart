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

using System;
using System.Collections.Generic;
using EvenCart.Core.Plugins;

namespace EvenCart.Infrastructure.Plugins
{
    public interface IPluginAccountant
    {
        IList<PluginInfo> GetInstalledPlugins();

        IList<PluginInfo> GetActivePlugins(Type type = null);

        IList<PluginInfo> GetAvailablePlugins(bool withWidgets = false);

        void InstallPlugin(PluginInfo pluginInfo);

        void UninstallPlugin(PluginInfo pluginInfo);

        void ActivatePlugin(PluginInfo pluginInfo);

        void DeactivatePlugin(PluginInfo pluginInfo);

        IList<WidgetInfo> GetAvailableWidgets();

        string AddWidget(string widgetName, string pluginSystemName, string zoneName);

        void DeleteWidget(string id);

        int GetActiveWidgetCount(string widgetZone);

        bool HandleZipUpload(byte[] fileBytes);
    }
}