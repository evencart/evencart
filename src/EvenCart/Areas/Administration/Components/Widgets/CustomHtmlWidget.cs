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
using Genesis.Extensions;
using Genesis.Helpers;
using Genesis.Infrastructure.Mvc;
using Genesis.Infrastructure.Mvc.Models;
using Genesis.Modules.Pluggable;
using Genesis.Plugins;
using Microsoft.AspNetCore.Mvc;

namespace EvenCart.Areas.Administration.Components.Widgets
{
    [ViewComponent(Name = WidgetSystemName)]
    public class CustomHtmlWidget : GenesisComponent, IWidget
    {
        private const string WidgetSystemName = "CustomHtml";
        private readonly IWidgetService _widgetService;
        public CustomHtmlWidget(IWidgetService widgetService)
        {
            _widgetService = widgetService;
        }

        public override IViewComponentResult Invoke(object data = null)
        {
            var dataAsDict = data as Dictionary<string, object>;
            if (dataAsDict == null)
                return R.Success.ComponentResult;
            var widgetId = dataAsDict["id"].ToString();
            var widgetSettings = _widgetService.LoadWidgetSettings<CustomHtmlWidgetSettings>(widgetId);
            if(!widgetSettings.Language.IsNullEmptyOrWhiteSpace() && widgetSettings.Language != Engine.CurrentLanguage.CultureCode)
                return R.Success.ComponentResult;
            string formattedContent = null;
            if (!widgetSettings.CustomFormat.IsNullEmptyOrWhiteSpace())
            {
                formattedContent = string.Format(widgetSettings.CustomFormat, widgetSettings.Title, widgetSettings.Content);
            }
            return R.Success.With("title", widgetSettings.Title)
                .With("content", widgetSettings.Content)
                .With("formattedContent", formattedContent)
                .With("widgetId", widgetId)
                .ComponentResult;
        }

        public string DisplayName { get; } = "Custom Html";

        public string SystemName { get; } = WidgetSystemName;

        public IList<string> WidgetZones { get; } = null;

        public bool HasConfiguration { get; } = true;

        public bool SkipDragging { get; } = false;

        public string ConfigurationUrl { get; } = null;

        public Type SettingsType { get; } = typeof(CustomHtmlWidgetSettings);

        public object GetViewObject(object settings)
        {
            var widgetSettings = settings as CustomHtmlWidgetSettings;
            var availableLanguages =
                SelectListHelper.GetSelectItemList(Engine.AllLanguages, x => x.CultureCode, x => x.Name);

            return new {
                title = widgetSettings?.Title,
                content = widgetSettings?.Content,
                customFormat = widgetSettings?.CustomFormat,
                language = widgetSettings?.Language,
                availableLanguages = availableLanguages
            };
        }

        public class CustomHtmlWidgetSettings : WidgetSettingsModel
        {
            public string Title { get; set; }

            public string Content { get; set; }

            public string CustomFormat { get; set; }

            public string Language { get; set; }
        }
    }
}