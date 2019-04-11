using System.Collections.Generic;
using EvenCart.Data.Enum;
using EvenCart.Infrastructure.Mvc.Models;

namespace EvenCart.Models.Products
{
    public class ProductSearchModel : PublicSearchModel
    {
        public decimal? FromPrice { get; set; }

        public decimal? ToPrice { get; set; }

        public int[] ManufacturerIds { get; set; }

        public int[] VendorIds { get; set; }

        public int? CategoryId { get; set; }

        public string SortColumn { get; set; }

        public SortOrder SortOrder { get; set; }

        public string Filters { get; set; }

        #region Virtual Properties

        public virtual decimal AvailableFromPrice { get; set; }

        public virtual decimal AvailableToPrice { get; set; }

        public virtual Dictionary<int, string> AvailableManufacturers { get; set; }

        public virtual Dictionary<int, string> AvailableVendors { get; set; }

        public virtual Dictionary<string, List<string>> AvailableFilters { get; set; }

        #endregion
    }
}