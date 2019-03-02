// #region Author Information
// // FoundationModelBinder.cs
// // 
// // (c) 2017 Apexol Technologies. All Rights Reserved.
// // 
// #endregion

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using RoastedMarketplace.Core.Infrastructure;
using RoastedMarketplace.Core.Infrastructure.Utils;
using RoastedMarketplace.Data.Entity.Settings;
using RoastedMarketplace.Infrastructure.Extensions;
using RoastedMarketplace.Infrastructure.Plugins;

namespace RoastedMarketplace.Infrastructure.Mvc.Models
{
    public class WidgetSettingsModelBinder : IModelBinder
    {
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
                //convert property value to appropriate type
                var typedValue = TypeConverter.CastPropertyValue(property, propertyValueProviderResult.FirstValue);
                property.SetValue(settingsObject, typedValue);
            }
            bindingContext.Result = ModelBindingResult.Success(settingsObject);
            return Task.CompletedTask;
        }
        
    }
}