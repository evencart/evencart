using System.Collections.Generic;
using System.Linq;
using EvenCart.Core;
using EvenCart.Infrastructure.Mvc.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EvenCart.Models.Components
{
    public class AttributeFilterModel : FoundationModel
    {
        public string FilterTitle { get; set; }

        public List<SelectListItem> FilterValues { get; set; }

        public string KeyName => CommonHelper.GenerateSlug(FilterTitle);

        public bool HasSelection => FilterValues?.Any(x => x.Selected) ?? false;
    }
}