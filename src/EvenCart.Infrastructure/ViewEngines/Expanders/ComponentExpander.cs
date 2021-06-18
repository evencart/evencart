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
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Genesis;
using Genesis.Infrastructure;
using EvenCart.Data.Extensions;
using EvenCart.Infrastructure.Mvc.Components;

namespace EvenCart.Infrastructure.ViewEngines.Expanders
{
    public class ComponentExpander : Expander
    {
        private const string AssignFormat = "{{%- assign {0} = {1} -%}}";
        public override string Expand(ReadFile readFile, Regex regEx, string inputContent, object parameters = null)
        {
            var componentMatches = regEx.Matches(inputContent);
            var viewComponentManager = DependencyResolver.Resolve<IViewComponentManager>();
            var componentIndexOnPage = 0;
            if (componentMatches.Count == 0)
            {
                foreach (var meta in readFile.GetMeta(nameof(ComponentExpander)))
                {
                    var componentName = meta.Key;
                    var valueAsArray = (object[]) meta.Value;
                    //extract values
                    componentIndexOnPage = (int) valueAsArray[0];
                    var keyValuePairs = (Dictionary<string, string>) valueAsArray[1];
                    var componentParameters = GetComponentParameters(keyValuePairs, parameters);
                    viewComponentManager.InvokeViewComponent(componentName, componentParameters, out string _, out object model, out string _, true);
                    MergeModel(parameters, model, componentName, componentIndexOnPage, out string _, out string _);
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
                    componentParameters,
                    out string componentContent, out object model, out string viewPath);

                if (!viewPath.IsNullEmptyOrWhiteSpace())
                    readFile.AddChild(ReadFile.From(viewPath));

                //merge models
                MergeModel(parameters, model, componentName, componentIndexOnPage, out var assignString, out var resetAssignString);
                if (!WebHelper.IsAjaxRequest(ApplicationEngine.CurrentHttpContext.Request))
                    //add keyvaluepairs as meta along with the index
                    readFile.AddMeta(componentName, new object[] { componentIndexOnPage, keyValuePairs}, nameof(ComponentExpander));
                var match = componentMatch.Result("$0");
                //replace only first occurance of the pattern result
                readFile.Content = readFile.Content.ReplaceFirstOccurance(match, assignString + componentContent + resetAssignString);
                inputContent = inputContent.ReplaceFirstOccurance(match, assignString + componentContent + resetAssignString);

                //next component
                componentIndexOnPage++;
            }
            return inputContent;
        }

        private static void MergeModel(object originalModel, object modelToMerge, string componentName, int componentIndexOnPage, out string assignString, out string resetAssignString)
        {
            //add to model
            var objects = originalModel as IDictionary<string, object>;
            var assignBuilder = new StringBuilder();
            var resetAssignBuilder = new StringBuilder();
            if (objects != null)
            {
                if (modelToMerge is IDictionary<string, object>)
                {
                    var modelToMergeAsDictionary = modelToMerge as IDictionary<string, object>;
                    foreach (var m in modelToMergeAsDictionary)
                    {
                        var key = $"{m.Key}{componentIndexOnPage}";
                        if (objects.ContainsKey(key))
                            objects[key] = m.Value;
                        else
                        {
                            objects.Add(key, m.Value);
                        }
                        assignBuilder.AppendFormat(AssignFormat, m.Key, key);
                        assignBuilder.AppendLine();
                        resetAssignBuilder.AppendFormat(AssignFormat, m.Key, "nil");
                        resetAssignBuilder.AppendLine();
                    }
                }
                else
                {
                    componentName = componentName.Replace('.', '_').Replace(" ", "_");
                    var key = $"{componentName}{componentIndexOnPage}";
                    if (objects.ContainsKey(key))
                        objects[key] = modelToMerge;
                    else
                        objects.Add($"{key}", modelToMerge);
                    assignBuilder.AppendFormat(AssignFormat, componentName, key);
                    resetAssignBuilder.AppendFormat(AssignFormat, componentName, string.Empty);
                }
            }
            assignString = assignBuilder.ToString();
            resetAssignString = resetAssignBuilder.ToString();
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
            if (keyValuePairs == null)
                return componentParameters;
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