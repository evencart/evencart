using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace RoastedMarketplace.Infrastructure.Routing
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
                area = "/" + ((AreaAttribute) areaAttribute).RouteValue;
            }
            
            foreach (var ar in attributeRoutes)
            {
                if (ar.AttributeRouteModel.Attribute is IDualRouteAttribute)
                {
                    var dualRouteAttribute = (IDualRouteAttribute) ar.AttributeRouteModel.Attribute;
                    if (dualRouteAttribute.OnlyApi)
                    {
                        var currentTemplate = ar.AttributeRouteModel.Template;
                        ar.AttributeRouteModel.Template = area + "/" + ApplicationConfig.ApiEndpointName + "/[controller]/" + currentTemplate;
                        ar.AttributeRouteModel.Name = $"{ApplicationConfig.ApiEndpointName}_" +
                                                      ar.AttributeRouteModel.Name;
                        continue; //skip adding anything else
                    }
                    //add another oute
                    var selectorModel = new SelectorModel(ar);
                    var newAm = selectorModel.AttributeRouteModel;
                    newAm.Template = area + "/" + ApplicationConfig.ApiEndpointName + "/[controller]/" + newAm.Template;
                    if(newAm.Name != null)
                        newAm.Name = $"{ApplicationConfig.ApiEndpointName}_" + newAm.Name;

                    newSelectors.Add(selectorModel);
                }
            }

            foreach (var ns in newSelectors)
                action.Selectors.Add(ns);
        }
    }
}