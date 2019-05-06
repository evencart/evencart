using System.Collections.Generic;
using EvenCart.Infrastructure.Mvc.Models;

namespace EvenCart.Models.Products
{
    public class ProductSpecificationModel : FoundationModel
    {
        public string Name { get; set; }

        public IList<string> Values { get; set; }

        public string ValuesCsv => string.Join(", ", Values);
    }
}