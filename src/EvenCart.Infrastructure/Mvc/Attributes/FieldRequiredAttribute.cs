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