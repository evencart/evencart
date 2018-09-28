using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using RoastedMarketplace.Data.Enum;
using RoastedMarketplace.Infrastructure.Extensions;

namespace RoastedMarketplace.Infrastructure.ViewEngines.GlobalObjects
{
    public class SelectOptionsObject : GlobalObject
    {
        public override object GetObject()
        {
            var selectOptions =
                new Dictionary<string, IList<SelectListItem>>
                {
                    {"inputTypes", EnumExtensions.GetSelectList(typeof(InputFieldType)).OrderBy(x => x.Text).ToList()}
                };
            return selectOptions;
        }
    }
}