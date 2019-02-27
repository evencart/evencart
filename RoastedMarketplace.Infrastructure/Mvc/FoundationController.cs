using System;
using Microsoft.AspNetCore.Mvc;
using RoastedMarketplace.Core.Infrastructure;
using RoastedMarketplace.Core.Services.Events;
using RoastedMarketplace.Infrastructure.Helpers;
using RoastedMarketplace.Infrastructure.Mvc.Breadcrumbs;

namespace RoastedMarketplace.Infrastructure.Mvc
{
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

        public string T(string resource, string languageCultureCode = "en-US", params object[] arguments)
        {
            return LocalizationHelper.Localize(resource, languageCultureCode, arguments);
        }

        public CustomResponse R => CustomResponse.Response(this);

        /// <summary>
        /// Raises a named event so other services and plugins can capture
        /// </summary>
        [NonAction]
        protected void RaiseEvent(Enum eventName, params object[] eventData)
        {
            DependencyResolver.Resolve<IEventPublisherService>().Publish(eventName.ToString(), eventData);
        }

        /// <summary>
        /// Creates a breadcrumb node with the provided data
        /// </summary>
        [NonAction]
        protected void SetBreadcrumbToUrl(string displayText, string url, string description = null, bool localize = true)
        {
            HttpContext.AppendToBreadcrumb(new BreadcrumbNode()
            {
                DisplayText = localize ? T(displayText, ApplicationEngine.CurrentLanguageCultureCode) : displayText,
                Url = url,
                Description = localize ? T(description, ApplicationEngine.CurrentLanguageCultureCode) : description
            });
        }

        /// <summary>
        /// Creates a breadcrumb node with the provided data
        /// </summary>
        [NonAction]
        protected void SetBreadcrumbToRoute(string displayText, string routeName, object routeParams = null, string description = null, bool localize = true)
        {
           SetBreadcrumbToUrl(displayText, Url.RouteUrl(routeName, routeParams), description, localize);
        }
    }
   
}