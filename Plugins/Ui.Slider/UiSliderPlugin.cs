using System.Collections.Generic;
using DotEntity.Versioning;
using RoastedMarketplace.Core.Plugins;
using RoastedMarketplace.Infrastructure;
using RoastedMarketplace.Services.Plugins;
using Ui.Slider.Versions;

namespace Ui.Slider
{
    public class UiSliderPlugin : DatabasePlugin
    {
        public override IList<IDatabaseVersion> GetDatabaseVersions()
        {
            var versions = base.GetDatabaseVersions();
            versions.Add(new Version_1_0());
            return versions;
        }

        public override string ConfigurationUrl =>
            ApplicationEngine.RouteUrl(UiSliderRouteNames.SlidesList);
    }
}
