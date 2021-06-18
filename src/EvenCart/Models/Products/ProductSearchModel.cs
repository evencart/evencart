#region License
// Copyright (c) Sojatia Infocrafts Private Limited.
// The following code is part of EvenCart eCommerce Software (https://evencart.co) Dual Licensed under the terms of
// 
// 1. GNU GPLv3 with additional terms (available to read at https://evencart.co/license)
// 2. EvenCart Proprietary License (available to read at https://evencart.co/license/commercial-license).
// 
// You can select one of the above two licenses according to your requirements. The usage of this code is
// subject to the terms of the license chosen by you.
#endregion

using System.Collections.Generic;
using Genesis.Infrastructure.Mvc.Models;
using Genesis.Modules.Meta;

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
        /// <summary>
        /// A list of tags to restrict the search to
        /// </summary>
        public IList<string> Tags { get; set; }

        #region Virtual Properties

        public virtual decimal AvailableFromPrice { get; set; }

        public virtual decimal AvailableToPrice { get; set; }

        public virtual Dictionary<int, string> AvailableManufacturers { get; set; }

        public virtual Dictionary<int, string> AvailableVendors { get; set; }

        public virtual Dictionary<string, List<string>> AvailableFilters { get; set; }

        #endregion
    }
}