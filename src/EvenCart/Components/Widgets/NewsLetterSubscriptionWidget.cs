using System;
using System.Collections.Generic;
using EvenCart.Core.Plugins;
using EvenCart.Infrastructure;
using EvenCart.Services.Navigation;
using EvenCart.Services.Products;
using EvenCart.Services.Widgets;
using EvenCart.Infrastructure.Mvc;
using EvenCart.Services.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace EvenCart.Areas.Administration.Components.Widgets
{
    [ViewComponent(Name = WidgetSystemName)]
    public class NewsletterSubscriptionWidget : FoundationComponent, IWidget
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
            var isLoggedIn = !ApplicationEngine.CurrentUser.IsVisitor();
            return R.Success.With("isLoggedIn", isLoggedIn)
                .With("subscriptionGuid", ApplicationConfig.NewsletterSubscriptionGuid).ComponentResult;
        }

        public string DisplayName { get; } = "Newsletter Subscription Form";

        public string SystemName { get; } = WidgetSystemName;

        public IList<string> WidgetZones { get; } = null;

        public bool HasConfiguration { get; } = false;

        public string ConfigurationUrl { get; } = null;

        public Type SettingsType { get; } = null;

        public object GetViewObject(object settings)
        {
            return settings;
        }
    }
}