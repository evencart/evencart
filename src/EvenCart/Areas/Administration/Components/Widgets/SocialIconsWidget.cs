﻿#region License
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
using Genesis.Infrastructure.Mvc;
using Genesis.Infrastructure.Mvc.Models;
using Genesis.Modules.Pluggable;
using Genesis.Plugins;
using Microsoft.AspNetCore.Mvc;

namespace EvenCart.Areas.Administration.Components.Widgets
{
    [ViewComponent(Name = WidgetSystemName)]
    public class SocialIconsWidget : GenesisComponent, IWidget
    {
        private const string WidgetSystemName = "SocialIcons";
        private readonly IWidgetService _widgetService;
        public SocialIconsWidget(IWidgetService widgetService)
        {
            _widgetService = widgetService;
        }

        public override IViewComponentResult Invoke(object data = null)
        {
            var dataAsDict = data as Dictionary<string, object>;
            if (dataAsDict == null)
                return R.Success.ComponentResult;
            var widgetId = dataAsDict["id"].ToString();
            var widgetSettings = _widgetService.LoadWidgetSettings<SocialIconsWidgetSettings>(widgetId);
            return R.Success.With("title", widgetSettings.Title)
                .With("facebookUrl", widgetSettings.FacebookUrl)
                .With("twitterUrl", widgetSettings.TwitterUrl)
                .With("youtubeUrl", widgetSettings.YoutubeUrl)
                .With("linkedInUrl", widgetSettings.LinkedInUrl)
                .With("instagramUrl", widgetSettings.InstagramUrl)
                .With("emailUrl", widgetSettings.EmailUrl)
                .With("rssFeedUrl", widgetSettings.RssFeedUrl)
                .With("skypeUrl", widgetSettings.SkypeUrl)
                .With("whatsappUrl", widgetSettings.WhatsAppUrl)
                .With("widgetId", widgetId)
                .ComponentResult;
        }

        public string DisplayName { get; } = "Social Icons";

        public string SystemName { get; } = WidgetSystemName;

        public IList<string> WidgetZones { get; } = null;

        public bool HasConfiguration { get; } = true;

        public bool SkipDragging { get; } = false;

        public string ConfigurationUrl { get; } = null;

        public Type SettingsType { get; } = typeof(SocialIconsWidgetSettings);

        public object GetViewObject(object settings)
        {
            var widgetSettings = settings as SocialIconsWidgetSettings;
            return new
            {
                title = widgetSettings?.Title,
                facebookUrl = widgetSettings?.FacebookUrl,
                twitterUrl = widgetSettings?.TwitterUrl,
                instagramUrl = widgetSettings?.InstagramUrl,
                linkedInUrl = widgetSettings?.LinkedInUrl,
                youtubeUrl = widgetSettings?.YoutubeUrl,
                rssfeedUrl = widgetSettings?.RssFeedUrl,
                whatsappUrl = widgetSettings?.WhatsAppUrl,
                skypeUrl = widgetSettings?.SkypeUrl,
                emailUrl = widgetSettings?.EmailUrl
            };
        }

        public class SocialIconsWidgetSettings : WidgetSettingsModel
        {
            public string Title { get; set; }

            public string FacebookUrl { get; set; }

            public string TwitterUrl { get; set; }

            public string InstagramUrl { get; set; }

            public string LinkedInUrl { get; set; }

            public string YoutubeUrl { get; set; }

            public string RssFeedUrl { get; set; }

            public string WhatsAppUrl { get; set; }

            public string SkypeUrl { get; set; }

            public string EmailUrl { get; set; }
        }
    }
}