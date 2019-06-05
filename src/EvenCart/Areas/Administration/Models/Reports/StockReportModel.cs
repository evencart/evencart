using System.Collections.Generic;
using EvenCart.Infrastructure.Mvc.Models;

namespace EvenCart.Areas.Administration.Models.Reports
{
    public class StockReportModel : FoundationModel
    {
        public string ProductName { get; set; }

        public int ProductId { get; set; }

        public int StockQuantity { get; set; }

        public int ReservedQuantity { get; set; }

        public bool Published { get; set; }

        public bool HasVariants { get; set; }

        public IList<VariantStockReportModel> Variants { get; set; }

        public class VariantStockReportModel : FoundationModel
        {
            public string AttributeText { get; set; }

            public int StockQuantity { get; set; }

            public int ReservedQuantity { get; set; }
        }
    }
}