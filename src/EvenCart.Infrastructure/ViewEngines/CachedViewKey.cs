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
using System.Linq;
using EvenCart.Infrastructure.Routing;

namespace EvenCart.Infrastructure.ViewEngines
{
    public class CachedViewKey : IEquatable<CachedViewKey>
    {
        public string ViewPath { get; set; }

        public string Url { get; set; }

        public string LanguageCultureCode { get; set; }

        public string Context { get; set; }

        public string Area { get; set; }

        public bool Equals(CachedViewKey other)
        {
            return string.Equals(ViewPath, other.ViewPath) && string.Equals(Url, other.Url) &&
                   string.Equals(LanguageCultureCode, other.LanguageCultureCode) && string.Equals(Area, other.Area);
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() != this.GetType()) return false;
            return Equals((CachedViewKey) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (ViewPath != null ? ViewPath.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Url != null ? Url.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (LanguageCultureCode != null ? LanguageCultureCode.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Area != null ? Area.GetHashCode() : 0);
                return hashCode;
            }
        }

        public static CachedViewKey Get(string viewPath, string languageCultureCode, string area)
        {
            //for creating cached view key, we need url of the page, 
            //we first get controller and action from view path and try to find the url for it.
            var url = viewPath;
            var context = "";
            if (viewPath.Contains("/"))
            {
                var viewPathParts = viewPath.Split("/", StringSplitOptions.RemoveEmptyEntries);
                if (viewPathParts.Length > 1)
                {
                    var controller = viewPathParts[0];
                    var action = viewPathParts[1];
                    var route = RouteFinder.GetAllRoutes(controller, area)
                        .FirstOrDefault(x => x.Action.Equals(action, StringComparison.InvariantCultureIgnoreCase));
                    if (route != null)
                    {
                        url = "/" + route.Template;
                    }
                    context = controller;
                }
            }
            return new CachedViewKey()
            {
                Url = url.ToLower(),
                LanguageCultureCode = languageCultureCode,
                ViewPath = viewPath,
                Context = context,
                Area = area
            };
        }
    }
}