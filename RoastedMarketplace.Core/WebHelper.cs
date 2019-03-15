using System;
using System.Collections.Generic;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using RoastedMarketplace.Core.Infrastructure;

namespace RoastedMarketplace.Core
{
    public class WebHelper
    {
        /// <summary>
        /// Parses a url for rendering
        /// </summary>
        /// <returns></returns>
        public static string GetUrlFromPath(string path, string rootDomain = "", string protocol = "http")
        {
            if (rootDomain.StartsWith("//"))
                rootDomain = protocol + ":" + rootDomain;
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
            return httpContextAccessor.HttpContext?.Connection.RemoteIpAddress.ToString() ?? "";
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