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
using System.Web;
using EvenCart.Core.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;

namespace EvenCart.Core
{
    public class WebHelper
    {
        /// <summary>
        /// Parses a url for rendering
        /// </summary>
        /// <returns></returns>
        public static string GetUrlFromPath(string path, string rootDomain = "", string protocol = "http")
        {
            if (path == null)
                return null;
            if (rootDomain.StartsWith("//"))
                rootDomain = protocol + ":" + rootDomain;
            if(!string.IsNullOrEmpty(rootDomain))
                if (!rootDomain.StartsWith("http") && !rootDomain.StartsWith("https") && !rootDomain.StartsWith("ftp"))
                    rootDomain = protocol + "://" + rootDomain;
            //we need to see if the path is relative or absolute
            if (path.StartsWith("~"))
            {
                //it's a relative path to server
                return rootDomain + path.Substring(1);
            }
            //it may be an absolute url
            return rootDomain + path;
        }

        /// <summary>
        /// Gets the client's ip address
        /// </summary>
        /// <returns></returns>
        public static string GetClientIpAddress()
        {
            var httpContextAccessor = DependencyResolver.Resolve<IHttpContextAccessor>();
            return httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString() ?? "";
        }

        /// <summary>
        /// Gets the current page url
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentUrl()
        {
            var httpContextAccessor = DependencyResolver.Resolve<IHttpContextAccessor>();
            return httpContextAccessor.HttpContext.Request.GetDisplayUrl();
        }

        /// <summary>
        /// Gets the referrer url 
        /// </summary>
        /// <returns></returns>
        public static string GetReferrerUrl()
        {
            var httpContextAccessor = DependencyResolver.Resolve<IHttpContextAccessor>();
            return httpContextAccessor.HttpContext.Request.Headers["Referer"].ToString();
        }
        /// <summary>
        /// Parses a url and returns a uri object
        /// </summary>
        /// <param name="url"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static Uri ParseUrl(string url, IDictionary<string, string> parameters = null)
        {
            try
            {
                var builder = new UriBuilder(url);
                if (parameters != null)
                {
                    var queryParams = HttpUtility.ParseQueryString(string.Empty);
                    foreach (var p in parameters)
                        queryParams[p.Key] = p.Value;

                    builder.Query = queryParams.ToString();
                }
                return builder.Uri;
            }
            catch (System.Exception)
            {
                return null;
            }
        }

        public static bool IsAjaxRequest(HttpRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (request.Headers != null)
                return request.Headers["X-Requested-With"] == "XMLHttpRequest";
            return false;
        }
    }
}