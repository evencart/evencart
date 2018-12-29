using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using RoastedMarketplace.Core;
using RoastedMarketplace.Infrastructure.Mvc.Models;

namespace RoastedMarketplace.Models.Components
{
    public class AttributeFilterModel : FoundationModel
    {
        public string FilterTitle { get; set; }

        public List<SelectListItem> FilterValues { get; set; }

        public string KeyName => CommonHelper.GenerateSlug(FilterTitle);

        public bool HasSelection => FilterValues?.Any(x => x.Selected) ?? false;
    }
}