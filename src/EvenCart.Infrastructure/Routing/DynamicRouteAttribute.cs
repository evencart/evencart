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
using Microsoft.AspNetCore.Mvc;

namespace EvenCart.Infrastructure.Routing
{
    [AttributeUsage(AttributeTargets.Method)]
    public class DynamicRouteAttribute : HttpGetAttribute
    {
        /// <summary>
        /// The setting name under UrlSettings group that contains the dynamic route template
        /// </summary>
        public string SettingName { get; set; }

        /// <summary>
        /// The entity type that the parameter to the action uses
        /// </summary>
        public string SeoEntityName { get; set; }

        /// <summary>
        /// The suffix that'll be added to the dynamic template. Default is blank.
        /// </summary>
        public string TemplateSuffix { get; set; } = "";

        /// <summary>
        /// The prefix that'll be added to the dynamic template. Default is blank.
        /// </summary>
        public string TemplatePrefix { get; set; } = "";
        /// <summary>
        /// The url template to be used. If this is set, the SettingName and TemplateSuffix and Prefix are ignored
        /// </summary>
        public string DynamicTemplate { get; set; }

        /// <summary>
        /// The parameter name passed to the method. Default is id
        /// </summary>
        public string ParameterName { get; set; } = "id";
    }
}