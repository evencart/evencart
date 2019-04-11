using System.Collections.Generic;
using EvenCart.Infrastructure.Mvc.Models;

namespace EvenCart.Areas.Administration.Models.Plugins
{
    public class WidgetInfoModel : FoundationModel
    {
        public string ZoneName { get; set; }

        public IList<WidgetModel> Widgets { get; set; }
    }
}