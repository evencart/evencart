using EvenCart.Data.Enum;
using EvenCart.Infrastructure.Mvc.Models;

namespace EvenCart.Areas.Administration.Models.Shop
{
    public class ProductSearchModel : AdminSearchModel
    {
        public int[] CategoryIds { get; set; }

        public int[] ManufacturerIds { get; set; }

        public int[] VendorIds { get; set; }

        public string SortColumn { get; set; }

        public SortOrder SortOrder { get; set; }

        public bool? Published { get; set; }

        public string[] Tags { get; set; }
    }
}