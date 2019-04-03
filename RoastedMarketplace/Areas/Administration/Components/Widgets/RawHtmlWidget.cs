using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using RoastedMarketplace.Core.Plugins;
using RoastedMarketplace.Data.Extensions;
using RoastedMarketplace.Infrastructure.Helpers;
using RoastedMarketplace.Infrastructure.Mvc;
using RoastedMarketplace.Infrastructure.Mvc.Models;
using RoastedMarketplace.Services.Gdpr;
using RoastedMarketplace.Services.Widgets;

namespace RoastedMarketplace.Areas.Administration.Components.Widgets
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
                if (!_gdprService.IsConsentAccepted(CurrentUser.Id, widgetSettings.AcceptedConsentId))
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