using System.Collections.Generic;
using EvenCart.Data.Enum;
using EvenCart.Infrastructure.Mvc.Models;

namespace EvenCart.Models.Products
{
    public class ProductSearchModel : PublicSearchModel
    {
        /// <summary>
        /// The lowest price for price range search
        /// </summary>
        public decimal? FromPrice { get; set; }
        /// <summary>
        /// The highest price for price range search
        /// </summary>
        public decimal? ToPrice { get; set; }
        /// <summary>
        /// A list of manufacturer ids to restrict the search to
        /// </summary>
        public int[] ManufacturerIds { get; set; }
        /// <summary>
        /// A list of vendor ids to restrict the search to
        /// </summary>
        public int[] VendorIds { get; set; }
        /// <summary>
        /// The category id if the search is restricted to a category
        /// </summary>
        public int? CategoryId { get; set; }
        /// <summary>
        /// The order of sorting the result. Can be one of name, createdon, price or popularity
        /// </summary>
        public string SortColumn { get; set; }
        /// <summary>
        /// The sort order of result
        /// </summary>
        public SortOrder SortOrder { get; set; }
        /// <summary>
        /// The filter string to filter products. The format should be encoded string of attribute-name1:value1,value2 attribute-name2:value3
        /// </summary>
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