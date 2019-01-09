using System.Collections.Generic;
using RoastedMarketplace.Infrastructure.Mvc.Models;

namespace RoastedMarketplace.Models.Products
{
    public class ProductSpecificationModel : FoundationModel
    {
        public string Name { get; set; }

        public IList<string> Values { get; set; }

        public string ValuesCsv => string.Join(", ", Values);
    }
}