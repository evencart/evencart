﻿#region License
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
using System.Linq;
using System.Threading.Tasks;
using Genesis.Data;
using Genesis.Infrastructure;
using Genesis.Infrastructure.Utils;
using EvenCart.Data.Entity.Settings;
using EvenCart.Infrastructure.Extensions;
using EvenCart.Infrastructure.Plugins;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace EvenCart.Infrastructure.Mvc.Models
{
    public class WidgetSettingsModelBinder : IModelBinder
    {
        private readonly IDataSerializer _dataSerializer;
        public WidgetSettingsModelBinder(IDataSerializer dataSerializer)
        {
            _dataSerializer = dataSerializer;
        }

        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (!typeof(WidgetSettingsModel).IsAssignableFrom(bindingContext.ModelType))
            {
                return null;
            }

            var modelName = "id";
            // Try to fetch the value of the argument by name
            var valueProviderResult =
                bindingContext.ValueProvider.GetValue(modelName);

            if (valueProviderResult == ValueProviderResult.None)
            {
                return Task.CompletedTask;
            }

            bindingContext.ModelState.SetModelValue(modelName,
                valueProviderResult);

            var id = valueProviderResult.FirstValue;

            // Check if the argument value is null or empty
            if (string.IsNullOrEmpty(id) || !Guid.TryParse(id, out Guid _))
            {
                return Task.CompletedTask;
            }

            var pluginSettings = DependencyResolver.Resolve<PluginSettings>();
            var widgetStatus = pluginSettings.GetSiteWidgets().FirstOrDefault(x => x.Id == id);
            if (widgetStatus == null)
                return Task.CompletedTask;
            var pluginAccountant = DependencyResolver.Resolve<IPluginAccountant>();
            var widget = pluginAccountant.GetAvailableWidgets()
                .FirstOrDefault(x => x.WidgetSystemName == widgetStatus.WidgetSystemName &&
                                     x.PluginSystemName == widgetStatus.PluginSystemName);

            if (widget == null || widget.WidgetInstance.SettingsType == null)
                return Task.CompletedTask;
            
            var settingsObject = Activator.CreateInstance(widget.WidgetInstance.SettingsType);
            //find properties and set if values are available
            foreach (var property in widget.WidgetInstance.SettingsType.GetProperties())
            {
                if (!property.CanWrite)
                    continue;
                var propertyValueProviderResult = bindingContext.ValueProvider.GetValue(property.Name);
                if (propertyValueProviderResult == ValueProviderResult.None)
                    continue;
                bindingContext.ModelState.SetModelValue(property.Name ,valueProviderResult);
                if (property.PropertyType.IsGenericType)
                {
                    //todo: find a better way...right now we just serialize and deserialize for quick type conversion
                    var targetValue = _dataSerializer.Deserialize(
                        _dataSerializer.Serialize(propertyValueProviderResult.Values), property.PropertyType);
                    property.SetValue(settingsObject, targetValue);
                }
                else
                {
                    var typedValue = TypeConverter.CastPropertyValue(property, propertyValueProviderResult.FirstValue);
                    property.SetValue(settingsObject, typedValue);
                }
            }
            bindingContext.Result = ModelBindingResult.Success(settingsObject);
            return Task.CompletedTask;
        }
        
    }
}