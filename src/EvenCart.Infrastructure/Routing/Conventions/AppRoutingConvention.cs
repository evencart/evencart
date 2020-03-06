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
using System.Linq;
using EvenCart.Core.Infrastructure;
using EvenCart.Core.Infrastructure.Routing;
using EvenCart.Core.Plugins;
using EvenCart.Data.Entity.Settings;
using EvenCart.Data.Extensions;
using EvenCart.Infrastructure.Mvc;
using EvenCart.Infrastructure.Mvc.Attributes;
using EvenCart.Infrastructure.Plugins;
using EvenCart.Services.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace EvenCart.Infrastructure.Routing.Conventions
{
    /// <summary>
    /// Adds additional routes for api access
    /// </summary>
    public class AppRoutingConvention : IActionModelConvention, IControllerModelConvention
    {
        public void Apply(ActionModel action)
        {
            var attributeRoutes = action.Selectors.Where(x => x.AttributeRouteModel != null).ToList();
            var newSelectors = new List<SelectorModel>();
            //check if it's within an area
            var areaAttribute = action.Controller.Attributes.FirstOrDefault(x => x.GetType() == typeof(AreaAttribute));
            var area = "";
            if (areaAttribute != null)
            {
                area = "/" + ((AreaAttribute)areaAttribute).RouteValue;
            }
            foreach (var ar in attributeRoutes)
            {
                if (!action.Properties.ContainsKey("RouteName"))
                    action.Properties.Add("RouteName", ar.AttributeRouteModel.Name);
                if (ar.AttributeRouteModel.Attribute is IDualRouteAttribute)
                {
                    var dualRouteAttribute = (IDualRouteAttribute)ar.AttributeRouteModel.Attribute;
                    if (dualRouteAttribute.OnlyApi)
                    {
                        var currentTemplate = ar.AttributeRouteModel.Template;
                        if (currentTemplate.StartsWith("~/"))
                            ar.AttributeRouteModel.Template =
                                area + "/" + ApplicationConfig.ApiEndpointName + "/" + currentTemplate.Replace("~/", "");
                        else
                            ar.AttributeRouteModel.Template =
                                area + "/" + ApplicationConfig.ApiEndpointName + "/[controller]/" + currentTemplate;
                        ar.AttributeRouteModel.Name = $"{ApplicationConfig.ApiEndpointName}_" +
                                                      ar.AttributeRouteModel.Name;
                        continue; //skip adding anything else
                    }
                    //add another route for api
                    var selectorModel = new SelectorModel(ar);
                    var newAm = selectorModel.AttributeRouteModel;
                    if (newAm.Template.StartsWith("~/"))
                        newAm.Template = area + "/" + ApplicationConfig.ApiEndpointName + "/" + newAm.Template.Replace("~/", "");
                    else
                        newAm.Template = area + "/" + ApplicationConfig.ApiEndpointName + "/[controller]/" + newAm.Template;
                    if (newAm.Name != null)
                        newAm.Name = $"{ApplicationConfig.ApiEndpointName}_" + newAm.Name;

                    newSelectors.Add(selectorModel);
                }
                else if (ar.AttributeRouteModel.Attribute is DynamicRouteAttribute)
                {
#if DEBUGWS
    continue;
#endif
                    var dynamicRoute = (DynamicRouteAttribute) ar.AttributeRouteModel.Attribute;
                    var template = dynamicRoute.DynamicTemplate;
                    if (template.IsNullEmptyOrWhiteSpace())
                    {
                        var settingService = DependencyResolver.Resolve<ISettingService>();
                        var settingName = dynamicRoute.SettingName;
                        var setting =
                            settingService.FirstOrDefault(x => x.GroupName == nameof(UrlSettings) && x.Key == settingName);
                        if (setting == null)
                            continue;
                        template = dynamicRoute.TemplatePrefix + setting.Value + dynamicRoute.TemplateSuffix;
                    }
                   
                    var dynamicRouteProvider = DependencyResolver.Resolve<IDynamicRouteProvider>();
                    dynamicRouteProvider.RegisterDynamicRoute(new RouteData()
                    {
                        Template = template,
                        ActionName = action.ActionName,
                        ControllerName = action.Controller.ControllerName,
                        RouteName = dynamicRoute.Name,
                        Order = dynamicRoute.Order,
                        SeoEntityName = dynamicRoute.SeoEntityName,
                        ParameterName = dynamicRoute.ParameterName
                    });
                }
            }
        
            foreach (var ns in newSelectors)
                action.Selectors.Add(ns);
        }

        private IList<PluginInfo> _activePlugins;
        public void Apply(ControllerModel controller)
        {
            
            if (typeof(FoundationPluginController).IsAssignableFrom(controller.ControllerType))
            {
                _activePlugins = _activePlugins ?? DependencyResolver.Resolve<IPluginAccountant>().GetActivePlugins();
                var pluginTypeAttribute = controller.Attributes.FirstOrDefault(x => x.GetType() == typeof(PluginTypeAttribute)) as PluginTypeAttribute;
                if (pluginTypeAttribute == null)
                    throw new ArgumentException(
                        $"The controller {controller.DisplayName} must declare a {nameof(PluginTypeAttribute)} with it's plugin type");

                if (_activePlugins.All(x => x.PluginType != pluginTypeAttribute.PluginType))
                    controller.Actions.Clear();
            }
        }
    }
}