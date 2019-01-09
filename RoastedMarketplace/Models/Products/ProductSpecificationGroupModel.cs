using System.Collections.Generic;
using RoastedMarketplace.Infrastructure.Mvc.Models;

namespace RoastedMarketplace.Models.Products
{
    public class ProductSpecificationGroupModel : FoundationModel
    {
        public string Name { get; set; }

        public List<ProductSpecificationModel> ProductSpecifications { get; set; }
    }
}