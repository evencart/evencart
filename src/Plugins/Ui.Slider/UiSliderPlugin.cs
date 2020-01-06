using System.Collections.Generic;
using DotEntity.Versioning;
using EvenCart.Core.Infrastructure;
using EvenCart.Services.Plugins;
using EvenCart.Infrastructure;
using EvenCart.Infrastructure.Plugins;
using EvenCart.Services.Settings;
using Ui.Slider.Components;
using Ui.Slider.Versions;

namespace Ui.Slider
{
    public class UiSliderPlugin : DatabasePlugin
    {
        private readonly ISettingService _settingService;
        public UiSliderPlugin(ISettingService settingService)
        {
            _settingService = settingService;
        }

        public override IList<IDatabaseVersion> GetDatabaseVersions()
        {
            var versions = base.GetDatabaseVersions();
            versions.Add(new Version1());
            return versions;
        }

        public override string ConfigurationUrl =>
            ApplicationEngine.RouteUrl(UiSliderRouteNames.SlidesList);

        public override void Install()
        {
            base.Install();
            //install the widget
            var widgetId = DependencyResolver.Resolve<IPluginAccountant>().AddWidget(SliderWidget.WidgetSystemName, "EvenCart.Ui.Slider", "slider");
            _settingService.Save(new UiSliderSettings()
            {
                WidgetId = widgetId
            });
        }

        public override void Uninstall()
        {
            base.Uninstall();
            var settings = DependencyResolver.Resolve<UiSliderSettings>();
            DependencyResolver.Resolve<IPluginAccountant>().DeleteWidget(settings.WidgetId);
            _settingService.DeleteSettings<UiSliderSettings>();
        }
    }
}
