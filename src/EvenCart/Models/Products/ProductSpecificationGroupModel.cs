using System.Collections.Generic;
using EvenCart.Infrastructure.Mvc.Models;

namespace EvenCart.Models.Products
{
    public class ProductSpecificationGroupModel : FoundationModel
    {
        public string Name { get; set; }

        public List<ProductSpecificationModel> ProductSpecifications { get; set; }
    }
}