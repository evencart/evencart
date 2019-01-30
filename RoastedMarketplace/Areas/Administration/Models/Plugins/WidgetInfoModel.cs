using System.Collections.Generic;
using RoastedMarketplace.Infrastructure.Mvc.Models;

namespace RoastedMarketplace.Areas.Administration.Models.Plugins
{
    public class WidgetInfoModel : FoundationModel
    {
        public string ZoneName { get; set; }

        public IList<WidgetModel> Widgets { get; set; }
    }
}