using System.Collections.Generic;
using EvenCart.Infrastructure.Mvc.Models;

namespace EvenCart.Areas.Administration.Models.Shop
{
    public class ProductSpecificationListModel : FoundationModel
    {
        public ProductSpecificationGroupModel ProductSpecificationGroup { get; set; }

        public IList<ProductSpecificationModel> ProductSpecifications { get; set; }

        public string ProductSpecificationsSerialized { get; set; }
    }
}