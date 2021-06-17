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
using EvenCart.Services.Products;
using Genesis.Infrastructure.Mvc;
using Genesis.Modules.Navigation;
using Genesis.Modules.Pluggable;
using Genesis.Modules.Users;
using Genesis.Plugins;
using Microsoft.AspNetCore.Mvc;

namespace EvenCart.Areas.Administration.Components.Widgets
{
    [ViewComponent(Name = WidgetSystemName)]
    public class NewsletterSubscriptionWidget : GenesisComponent, IWidget
    {
        private const string WidgetSystemName = "NewsletterSubscription";
        private readonly IWidgetService _widgetService;
        public NewsletterSubscriptionWidget(IWidgetService widgetService, IMenuService menuService, ICategoryService categoryService)
        {
            _widgetService = widgetService;
        }

        public override IViewComponentResult Invoke(object data = null)
        {
            var dataAsDict = data as Dictionary<string, object>;
            if (dataAsDict == null)
                return R.Success.ComponentResult;
            var isLoggedIn = !Engine.CurrentUser.IsVisitor();
            return R.Success.With("isLoggedIn", isLoggedIn)
                .With("subscriptionGuid", Engine.StaticConfig.NewsletterSubscriptionGuid).ComponentResult;
        }

        public string DisplayName { get; } = "Newsletter Subscription Form";

        public string SystemName { get; } = WidgetSystemName;

        public IList<string> WidgetZones { get; } = null;

        public bool HasConfiguration { get; } = false;

        public bool SkipDragging { get; } = false;

        public string ConfigurationUrl { get; } = null;

        public Type SettingsType { get; } = null;

        public object GetViewObject(object settings)
        {
            return settings;
        }
    }
}