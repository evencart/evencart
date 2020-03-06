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
using EvenCart.Core.Plugins;

namespace EvenCart.Infrastructure.Mvc.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class PluginTypeAttribute : Attribute
    {
        private Type _pluginType;

        public Type PluginType
        {
            get => _pluginType;
            set
            {
                if(!typeof(IPlugin).IsAssignableFrom(value))
                    throw new ArgumentException($"The plugin type must be a class implementing {nameof(IPlugin)}. Provide value {_pluginType} doesn't implement {nameof(IPlugin)}");
                _pluginType = value;
            }
        }
    }
}