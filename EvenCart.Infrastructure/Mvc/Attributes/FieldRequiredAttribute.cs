using System;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Routing;

namespace EvenCart.Infrastructure.Mvc.Attributes
{
    public class FieldRequiredAttribute : ActionMethodSelectorAttribute
    {
        private readonly string _keyName;
        private readonly string _keyValue;

        public FieldRequiredAttribute(string keyName) : this(keyName, null)
        {
        }

        public FieldRequiredAttribute(string keyName, string keyValue)
        {
            _keyName = keyName;
            _keyValue = keyValue;
        }

        public override bool IsValidForRequest(RouteContext routeContext, ActionDescriptor action)
        {
            if (routeContext.HttpContext.Request.Method != WebRequestMethods.Http.Post)
                return false;

            if(_keyValue == null)
                if (routeContext.HttpContext.Request.Form.Keys.Any(x => x.Equals(_keyValue,
                    StringComparison.InvariantCultureIgnoreCase)))
                    return true;

            string value = routeContext.HttpContext.Request.Form[_keyName];
            return value == _keyValue;
        }
    }
}