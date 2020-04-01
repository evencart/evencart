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
using EvenCart.Core.Plugins;
using EvenCart.Infrastructure.Mvc;
using EvenCart.Infrastructure.Mvc.Models;
using EvenCart.Services.Widgets;
using Microsoft.AspNetCore.Mvc;

namespace EvenCart.Areas.Administration.Components.Widgets
{
    [ViewComponent(Name = WidgetSystemName)]
    public class ShareThisWidget : FoundationComponent, IWidget
    {
        private const string WidgetSystemName = "ShareThis";
        private readonly IWidgetService _widgetService;
        public ShareThisWidget(IWidgetService widgetService)
        {
            _widgetService = widgetService;
        }

        public override IViewComponentResult Invoke(object data = null)
        {
            var dataAsDict = data as Dictionary<string, object>;
            if (dataAsDict == null)
                return R.Success.ComponentResult;
            var widgetId = dataAsDict["id"].ToString();
            var widgetSettings = _widgetService.LoadWidgetSettings<ShareThisWidget.ShareThisWidgetSettings>(widgetId);
            return R.Success.With("title", widgetSettings.Title)
                .With("sticky", widgetSettings.Sticky)
                .With("widgetId", widgetId)
                .With("propertyId", widgetSettings.PropertyId)
                .With("productType", !widgetSettings.Sticky ? "inline-share-buttons" : "sticky-share-buttons")
                .ComponentResult;
        }

        public string DisplayName { get; } = "Share This";

        public string SystemName { get; } = WidgetSystemName;

        public IList<string> WidgetZones { get; } = null;

        public bool HasConfiguration { get; } = true;

        public bool SkipDragging { get; } = false;

        public string ConfigurationUrl { get; } = null;

        public Type SettingsType { get; } = typeof(ShareThisWidget.ShareThisWidgetSettings);

        public object GetViewObject(object settings)
        {
            var widgetSettings = settings as ShareThisWidget.ShareThisWidgetSettings;
            return new
            {
                title = widgetSettings?.Title,
                sticky = widgetSettings?.Sticky,
                propertyId = widgetSettings?.PropertyId,
            };
        }

        public class ShareThisWidgetSettings : WidgetSettingsModel
        {
            public string Title { get; set; }

            public bool Sticky { get; set; }

            public string PropertyId { get; set; }
        }
    }
}