using EvenCart.Infrastructure.Mvc.Models;

namespace EvenCart.Areas.Administration.Models.Shop
{
    public class ProductRelationModel : FoundationEntityModel
    {
        public bool IsReciprocal { get; set; }

        public ProductModel DestinationProduct { get; set; }
    }
}