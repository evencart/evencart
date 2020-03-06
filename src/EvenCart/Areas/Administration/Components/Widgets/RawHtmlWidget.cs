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
using System.Linq;
using EvenCart.Core.Plugins;
using EvenCart.Services.Gdpr;
using EvenCart.Services.Widgets;
using EvenCart.Infrastructure.Helpers;
using EvenCart.Infrastructure.Mvc;
using EvenCart.Infrastructure.Mvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace EvenCart.Areas.Administration.Components.Widgets
{
    [ViewComponent(Name = WidgetSystemName)]
    public class RawHtmlWidget : FoundationComponent, IWidget
    {
        private const string WidgetSystemName = "RawHtml";
        private readonly IWidgetService _widgetService;
        private readonly IGdprService _gdprService;
        private readonly IConsentService _consentService;
        public RawHtmlWidget(IWidgetService widgetService, IGdprService gdprService, IConsentService consentService)
        {
            _widgetService = widgetService;
            _gdprService = gdprService;
            _consentService = consentService;
        }

        public override IViewComponentResult Invoke(object data = null)
        {
            var dataAsDict = data as Dictionary<string, object>;
            if (dataAsDict == null)
                return R.Success.ComponentResult;
            var widgetId = dataAsDict["id"].ToString();
            var widgetSettings = _widgetService.LoadWidgetSettings<RawHtmlWidgetSettings>(widgetId);
            //validate consent if required
            if (widgetSettings.AcceptedConsentId > 0)
            {
                if (CurrentUser == null || !_gdprService.IsConsentAccepted(CurrentUser.Id, widgetSettings.AcceptedConsentId))
                {
                    return R.Success.ComponentResult;
                }
            }
            return R.Success
                .With("content", widgetSettings.Content)
                .With("widgetId", widgetId)
                .ComponentResult;
        }

        public string DisplayName { get; } = "Raw Html";

        public string SystemName { get; } = WidgetSystemName;

        public IList<string> WidgetZones { get; } = null;

        public bool HasConfiguration { get; } = true;

        public bool SkipDragging { get; } = false;

        public string ConfigurationUrl { get; } = null;

        public Type SettingsType { get; } = typeof(RawHtmlWidgetSettings);

        public object GetViewObject(object settings)
        {
            var widgetSettings = settings as RawHtmlWidgetSettings;
            //get consents
            var consents = _consentService.Get(x => true).ToList();
            var consentsSelector = SelectListHelper.GetSelectItemList(consents, x => x.Id, x => x.Title);
            return new {
                content = widgetSettings?.Content,
                acceptedConsentId = widgetSettings?.AcceptedConsentId ?? 0,
                availableConsents = consentsSelector
            };
        }

        public class RawHtmlWidgetSettings : WidgetSettingsModel
        {
            public string Content { get; set; }

            public int AcceptedConsentId { get; set; }
        }
    }
}