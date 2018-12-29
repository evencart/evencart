using RoastedMarketplace.Data.Enum;
using RoastedMarketplace.Infrastructure.Mvc.Models;

namespace RoastedMarketplace.Areas.Administration.Models.Shop
{
    public class ProductSearchModel : AdminSearchModel
    {
        public int[] CategoryIds { get; set; }

        public int[] ManufacturerIds { get; set; }

        public int[] VendorIds { get; set; }

        public string SortColumn { get; set; }

        public SortOrder SortOrder { get; set; }

        public bool? Published { get; set; }
    }
}