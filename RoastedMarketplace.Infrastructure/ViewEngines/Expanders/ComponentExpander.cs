using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using RoastedMarketplace.Core.Extensions;
using RoastedMarketplace.Core.Infrastructure;
using RoastedMarketplace.Infrastructure.Mvc.Components;

namespace RoastedMarketplace.Infrastructure.ViewEngines.Expanders
{
    public class ComponentExpander : Expander
    {
        public override string Expand(ReadFile readFile, Regex regEx, string inputContent, object parameters = null)
        {
            var componentMatches = regEx.Matches(inputContent);
            var viewComponentManager = DependencyResolver.Resolve<IViewComponentManager>();
            if (componentMatches.Count == 0)
            {
                foreach (var meta in readFile.GetMeta(nameof(ComponentExpander)))
                {
                    var componentName = meta.Key;
                    var keyValuePairs = (Dictionary<string, string>) meta.Value;
                    var componentParameters = GetComponentParameters(keyValuePairs, parameters);
                    viewComponentManager.InvokeViewComponent(componentName, componentParameters.Count > 1 ? (object) componentParameters : (object)componentParameters.FirstOrDefault().Value ?? null, out string _, out object model, out string _, true);
                    MergeModel(parameters, model, componentName);
                }
                return inputContent;
            }


            foreach (Match componentMatch in componentMatches)
            {
                ExtractMatch(componentMatch, out string[] straightParameters, out Dictionary<string, string> keyValuePairs);
                if (!straightParameters.Any())
                    throw new Exception($"A component must be specified with component tag in view {readFile.FileName}");

                var componentName = straightParameters[0];
                //collect the values that are being passed to the component
                var componentParameters = GetComponentParameters(keyValuePairs, parameters);
                viewComponentManager.InvokeViewComponent(componentName,
                    componentParameters.Count > 1 ? componentParameters : (object) componentParameters.FirstOrDefault().Value,
                    out string componentContent, out object model, out string viewPath);

                if (!viewPath.IsNullEmptyOrWhitespace())
                    readFile.AddChild(ReadFile.From(viewPath));

                //add keyvaluepairs as meta
                readFile.AddMeta(componentName, keyValuePairs, nameof(ComponentExpander));
               
                readFile.Content = readFile.Content.Replace(componentMatch.Result("$0"), componentContent);
                inputContent = inputContent.Replace(componentMatch.Result("$0"), componentContent);
                //merge models
                MergeModel(parameters, model, componentName);
            }
            return inputContent;
        }

        private void MergeModel(object originalModel, object modelToMerge, string componentName)
        {
            //add to model
            var objects = originalModel as IDictionary<string, object>;
            if (objects != null)
            {
                if (modelToMerge is IDictionary<string, object>)
                {
                    var modelToMergeAsDictionary = modelToMerge as IDictionary<string, object>;
                    foreach (var m in modelToMergeAsDictionary)
                    {
                        if (objects.ContainsKey(m.Key))
                            objects[m.Key] = m.Value;
                        else
                        {
                            objects.Add(m.Key, m.Value);
                        }
                    }
                }
                else
                {
                    objects.Add(componentName, modelToMerge);
                }
            }
        }

        private object ExtractObject(object originalModel, string objectPath)
        {
            if (originalModel == null)
                return null;
            if (objectPath.StartsWith("@"))
                objectPath = objectPath.Substring(1);
            var parts = objectPath.Trim('.').Split('.', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 0)
                return null;
            var objects = originalModel as IDictionary<string, object>;
            object targetObject = null;
            if (objects != null)
            {
                if (!objects.ContainsKey(parts[0]))
                    return null;
                targetObject = objects[parts[0]];
            }
            else
            {
                var property = originalModel.GetType().GetProperty(parts[0], BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                if (property != null)
                {
                    targetObject = property.GetValue(originalModel);
                }
            }
            if (parts.Length > 1)
            {
                var subPath = objectPath.Substring(objectPath.IndexOf('.'));
                targetObject = ExtractObject(targetObject, subPath) ?? targetObject;
            }

            return targetObject;
        }

        private Dictionary<string, object> GetComponentParameters(Dictionary<string, string> keyValuePairs, object parameters)
        {
            //collect the values that are being passed to the component
            var componentParameters = new Dictionary<string, object>();
            foreach (var kp in keyValuePairs)
            {
                if (!kp.Value.StartsWith("@"))
                    componentParameters.Add(kp.Key, kp.Value);
                else
                {
                    componentParameters.Add(kp.Key, ExtractObject(parameters, kp.Value));
                }
            }

            return componentParameters;
        }
    }
}