using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using RoastedMarketplace.Core.Infrastructure;
using RoastedMarketplace.Core.Infrastructure.Routing;
using RoastedMarketplace.Data.Entity.Settings;
using RoastedMarketplace.Services.Settings;

namespace RoastedMarketplace.Infrastructure.Routing.Conventions
{
    /// <summary>
    /// Adds additional routes for api access
    /// </summary>
    public class AppRoutingConvention : IActionModelConvention
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
                        ar.AttributeRouteModel.Template = area + "/" + ApplicationConfig.ApiEndpointName + "/[controller]/" + currentTemplate;
                        ar.AttributeRouteModel.Name = $"{ApplicationConfig.ApiEndpointName}_" +
                                                      ar.AttributeRouteModel.Name;
                        continue; //skip adding anything else
                    }
                    //add another route for api
                    var selectorModel = new SelectorModel(ar);
                    var newAm = selectorModel.AttributeRouteModel;
                    newAm.Template = area + "/" + ApplicationConfig.ApiEndpointName + "/[controller]/" + newAm.Template;
                    if (newAm.Name != null)
                        newAm.Name = $"{ApplicationConfig.ApiEndpointName}_" + newAm.Name;

                    newSelectors.Add(selectorModel);
                }
                else if (ar.AttributeRouteModel.Attribute is DynamicRouteAttribute)
                {
                    var dynamicRoute = (DynamicRouteAttribute) ar.AttributeRouteModel.Attribute;
                    var settingService = DependencyResolver.Resolve<ISettingService>();
                    var settingName = dynamicRoute.SettingName;
                    var setting =
                        settingService.FirstOrDefault(x => x.GroupName == nameof(UrlSettings) && x.Key == settingName);
                    if (setting == null)
                        continue;
                    var dynamicRouteProvider = DependencyResolver.Resolve<IDynamicRouteProvider>();
                    dynamicRouteProvider.RegisterDynamicRoute(new RouteData()
                    {
                        Template = dynamicRoute.TemplatePrefix + setting.Value + dynamicRoute.TemplateSuffix,
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
    }
}