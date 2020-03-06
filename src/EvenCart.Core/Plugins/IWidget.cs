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