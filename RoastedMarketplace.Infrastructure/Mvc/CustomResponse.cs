using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace RoastedMarketplace.Infrastructure.Mvc
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
        /// <summary>
        /// Attaches specified parameter to the response
        /// </summary>
        /// <param name="key">The key of the parameter</param>
        /// <param name="apiAction">The function that'll be called if request is an api request</param>
        /// <param name="mvcAction">The function that'll be called if request is a mvc request</param>
        /// <returns>The response object</returns>
        public CustomResponse With(string key, Func<object> apiAction, Func<object> mvcAction)
        {
            if (RequestHelper.IsApiCall())
            {
                if (apiAction != null)
                    With(key, apiAction());
            }
            else
            {
                if (mvcAction != null)
                    With(key, mvcAction());
            }
            return this;
        }

        public static CustomResponse Response(Controller controller) => new CustomResponse(controller);

        public static CustomResponse ComponentResponse(ViewComponent component) => new CustomResponse(component);

        public CustomResponse Success => With("success", true);

        public CustomResponse Fail => With("success", false);

        public IActionResult Result => GetResult(this);

        public IViewComponentResult ComponentResult => GetComponentResult(this);

        public string Area { get; set; }

        public IActionResult Redirect(string url)
        {
            With("redirect", true);
            With("url", url);
            return Result;
        }

        public static implicit operator ExpandoObject(CustomResponse r)
        {
            return r._expandoObject;
        }

        public static IActionResult GetResult(CustomResponse r)
        {
            if (RequestHelper.IsApiCall())
            {
                //ignore the view and return the model as json
                return r._controller.Json(r._expandoObject);
            }
            if(r._viewName != null)
                return r._controller?.View(r._viewName, r._expandoObject);
            return r._controller?.View(r._expandoObject);
        }

        public static IViewComponentResult GetComponentResult(CustomResponse r)
        {
            if (r._viewName != null)
                return r._viewComponent?.View(r._viewName, r._expandoObject);
            return r._viewComponent?.View(r._expandoObject);
        }

    }
}