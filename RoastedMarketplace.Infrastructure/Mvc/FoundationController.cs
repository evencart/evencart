using System;
using System.Collections.Generic;
using System.Dynamic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace RoastedMarketplace.Infrastructure.Mvc
{
    [Route("[controller]")]
    public abstract class FoundationController : Controller
    {
        protected IActionResult Result(object model = null)
        {
            if (RequestHelper.IsApiCall())
            {
                //ignore the view and return the model as json
                return Json(model);
            }
            return View(model);
        }

        protected IActionResult Result(IActionResult mvcResult, IActionResult apiResult)
        {
            return RequestHelper.IsApiCall() ? apiResult : mvcResult;
        }

        protected IActionResult ContentResult(string content)
        {
            if (RequestHelper.IsApiCall())
                return Json(new { content });
            return Content(content);
        }

        public string T(string resource, string languageCultureCode = "en-US")
        {
            return resource;
        }

        public CustomResponse R => CustomResponse.Response(this);

        public sealed class CustomResponse
        {
            private readonly ExpandoObject _expandoObject;
            private readonly Controller _controller;
            private CustomResponse(Controller controller)
            {
                _controller = controller;
                //get controller name
                var controllerName = ApplicationEngine.CurrentHttpContext.GetRouteData().Values["controller"].ToString().ToLower();
                _expandoObject = new ExpandoObject();
                With("context", controllerName);
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

            public CustomResponse Success => With("success", true);

            public CustomResponse Fail => With("success", false);

            public IActionResult Result => GetResult(this);

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
                return r._controller.View(r._expandoObject);
            }

        }
    }
}