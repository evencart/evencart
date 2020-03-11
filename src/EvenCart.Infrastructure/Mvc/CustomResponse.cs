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
using System.Dynamic;
using System.Linq;
using System.Reflection;
using EvenCart.Data.Extensions;
using EvenCart.Infrastructure.Extensions;
using EvenCart.Infrastructure.Mvc.Attributes;
using EvenCart.Infrastructure.Mvc.Models;
using EvenCart.Infrastructure.ViewEngines.GlobalObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace EvenCart.Infrastructure.Mvc
{
    public sealed class CustomResponse
    {
        private readonly ExpandoObject _expandoObject;
        private readonly Controller _controller;
        private readonly ViewComponent _viewComponent;
        private static readonly string[] IgnoredRouteValues = { "action", "area", "controller" };

        private CustomResponse(Controller controller)
        {
            _controller = controller;
            //get controller name
            var controllerName = ApplicationEngine.CurrentHttpContext.GetRouteData().Values["controller"].ToString().ToLower();
            Area = ApplicationEngine.CurrentHttpContext.GetRouteData().Values["area"]?.ToString().ToLower();
            _expandoObject = new ExpandoObject();
            With("context", controllerName);
            //passed values except the ones ignored
            foreach (var rv in controller.RouteData.Values.Where(x => !IgnoredRouteValues.Contains(x.Key)))
            {
                With(rv.Key, rv.Value);
            }
        }

        private CustomResponse(ViewComponent component)
        {
            _viewComponent = component;
            _expandoObject = new ExpandoObject();
        }

        public CustomResponse With(string key, object value)
        {
            var expandoDict = (IDictionary<string, object>)_expandoObject;
            if (expandoDict.ContainsKey(key))
                expandoDict[key] = value;
            else
                expandoDict.Add(key, value);
            return this;
        }

        private string _viewName = null;
        public CustomResponse WithView(string viewName)
        {
            _viewName = viewName;
            return this;
        }

        public static CustomResponse Response(Controller controller) => new CustomResponse(controller);

        public static CustomResponse ComponentResponse(ViewComponent component) => new CustomResponse(component);

        public CustomResponse Success => With("success", true);

        public CustomResponse Fail => With("success", false);

        public IActionResult Result => GetResult(this);

        public IViewComponentResult ComponentResult => GetComponentResult(this);

        public string Area { get; set; }

        public CustomResponse Redirect(string url)
        {
            With("redirect", true);
            With("url", url);
            return this;
        }

        public static implicit operator ExpandoObject(CustomResponse r)
        {
            return r._expandoObject;
        }

        public static IActionResult GetResult(CustomResponse r)
        {
            var expandoDict = (IDictionary<string, object>)r._expandoObject;
            var allValues = expandoDict.Values.Where(x => x != null && !x.GetType().IsPrimitive);
            foreach (var value in allValues)
                CheckForFormattedValues(value);
            if (RequestHelper.IsApiCall(out bool withStoreMeta, out string[] metaTypes))
            {
                if (withStoreMeta)
                {
                    foreach (var metaType in metaTypes)
                    {
                        var metaObject = GlobalObject.ExecuteObject(metaType);
                        if (metaObject != null)
                            r.With(metaType, metaObject);
                    }
                }
                //ignore the view and return the model as json
                return r._controller.Json(r._expandoObject);
            }
            if (r._viewName != null)
                return r._controller?.View(r._viewName, r._expandoObject);
            return r._controller?.View(r._expandoObject);
        }

        public static IViewComponentResult GetComponentResult(CustomResponse r)
        {
            if (r._viewName != null)
                return r._viewComponent?.View(r._viewName, r._expandoObject);
            return r._viewComponent?.View(r._expandoObject);
        }

        #region Helpers

        /// <summary>
        /// The method checks for <see cref="FormatAsCurrenciesAttribute"/> in the object and adds additional properties to the responses to represent formatted data
        /// </summary>
        private static void CheckForFormattedValues(object value, IList<PropertyInfo> attributeProperties = null, string currency = null, PropertyInfo currencyCodeProperty = null)
        {
            if (value == null)
                return;
            var typeOfFm = value.GetType();
            if (value is IEnumerable<FoundationModel> valueAsCollection)
            {
                var genericTypeArgument = typeOfFm.GenericTypeArguments[0];
                var properties = GetFormattableProperties(genericTypeArgument, out currencyCodeProperty);
                foreach (var collectionValue in valueAsCollection)
                    CheckForFormattedValues(collectionValue, properties, currency, currencyCodeProperty);
            }
            else if (value is FoundationModel valueAsFm)
            {
                var properties = attributeProperties ?? GetFormattableProperties(typeOfFm, out currencyCodeProperty);
                currency = currencyCodeProperty?.GetValue(value)?.ToString() ?? currency;
                foreach (var fp in properties)
                {
                    var fpValue = fp.GetValue(value, null);
                    if (fpValue == null)
                        continue;
                    if (typeof(DateTime).IsAssignableFrom(fp.PropertyType) ||
                        typeof(DateTime?).IsAssignableFrom(fp.PropertyType))
                    {
                        valueAsFm.Formatted.Set(fp.Name.ToCamelCase(), ((DateTime)fpValue).ToFormattedString(false));
                    }
                    else
                    {
                        valueAsFm.Formatted.Set(fp.Name.ToCamelCase(), ((decimal)fpValue).ToCurrency(currency));
                    }
                }

                //any child properties
                var list = typeOfFm.GetProperties().Where(x =>
                    typeof(IEnumerable<FoundationModel>).IsAssignableFrom(x.PropertyType) ||
                    typeof(FoundationModel).IsAssignableFrom(x.PropertyType)).ToList();
                foreach (var innerProperty in list)
                {
                    CheckForFormattedValues(innerProperty.GetValue(value), null, currency, currencyCodeProperty);
                }
            }
           

        }

        private static IList<PropertyInfo> GetFormattableProperties(Type type, out PropertyInfo currencyCodeProperty)
        {
            currencyCodeProperty = null;
            var attribute = type.GetCustomAttributes(typeof(FormatAsCurrenciesAttribute))
                .Select(x => (FormatAsCurrenciesAttribute) x).FirstOrDefault();
            var propertyNames = attribute?.PropertyNames.Distinct() ?? new List<string>();
            var allProperties = type.GetProperties();
            var properties = allProperties.Where(x =>
                typeof(DateTime).IsAssignableFrom(x.PropertyType) ||
                typeof(DateTime?).IsAssignableFrom(x.PropertyType) ||
                propertyNames.Contains(x.Name)).ToList();
            if (attribute != null && !attribute.CurrencyCodeProperty.IsNullEmptyOrWhiteSpace())
            {
                currencyCodeProperty = allProperties.FirstOrDefault(x => x.Name == attribute.CurrencyCodeProperty);
                if (currencyCodeProperty == null)
                    throw new Exception($"The property {attribute.CurrencyCodeProperty} was not found on type {type.Name}");
            }
            return properties;
        }
        #endregion
    }
}