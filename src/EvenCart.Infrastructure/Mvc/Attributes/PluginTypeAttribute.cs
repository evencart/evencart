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