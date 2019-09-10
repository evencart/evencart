using System.Collections.Generic;
using System.Linq;
using EvenCart.Core.Infrastructure;
using EvenCart.Core.Infrastructure.Routing;
using EvenCart.Data.Entity.Settings;
using EvenCart.Data.Extensions;
using EvenCart.Services.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace EvenCart.Infrastructure.Routing.Conventions
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
    }
}