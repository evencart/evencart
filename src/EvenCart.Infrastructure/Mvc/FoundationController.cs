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
using EvenCart.Core.Infrastructure;
using EvenCart.Core.Services.Events;
using EvenCart.Core.Services.Interceptor;
using EvenCart.Data.Entity.Shop;
using EvenCart.Data.Entity.Users;
using EvenCart.Infrastructure.Extensions;
using EvenCart.Infrastructure.Helpers;
using EvenCart.Infrastructure.Mvc.Breadcrumbs;
using Microsoft.AspNetCore.Mvc;

namespace EvenCart.Infrastructure.Mvc
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

        [NonAction]
        public string T(string resource, string languageCultureCode = "en-US", params object[] arguments)
        {
            return LocalizationHelper.Localize(resource, languageCultureCode, arguments);
        }

        public CustomResponse R => CustomResponse.Response(this);

        protected User CurrentUser => ApplicationEngine.CurrentUser;

        protected Store CurrentStore => ApplicationEngine.CurrentStore;

        /// <summary>
        /// Raises a named event so other services and plugins can capture
        /// </summary>
        [NonAction]
        protected void RaiseEvent(Enum eventName, params object[] eventData)
        {
            RaiseEvent(eventName.ToString(), eventData);
        }

        /// <summary>
        /// Raises a named event so other services and plugins can capture
        /// </summary>
        [NonAction]
        protected void RaiseEvent(string eventName, params object[] eventData)
        {
            DependencyResolver.Resolve<IEventPublisherService>().Publish(eventName, eventData);
        }

        [NonAction]
        protected void Intercept(string location, params object[] parameters)
        {
            DependencyResolver.Resolve<IInterceptorService>().Intercept(location, parameters);
        }
        /// <summary>
        /// Creates a breadcrumb node with the provided data
        /// </summary>
        [NonAction]
        protected void SetBreadcrumbToUrl(string displayText, string url, string description = null, bool localize = true)
        {
            HttpContext.AppendToBreadcrumb(new BreadcrumbNode()
            {
                DisplayText = localize ? T(displayText, ApplicationEngine.CurrentLanguage.CultureCode) : displayText,
                Url = url,
                Description = localize ? T(description, ApplicationEngine.CurrentLanguage.CultureCode) : description
            });
        }

        /// <summary>
        /// Creates a breadcrumb node with the provided data
        /// </summary>
        [NonAction]
        protected void SetBreadcrumbToRoute(string displayText, string routeName, object routeParams = null, string description = null, bool localize = true)
        {
           SetBreadcrumbToUrl(displayText, ApplicationEngine.RouteUrl(routeName, routeParams, true), description, localize);
        }
    }
   
}