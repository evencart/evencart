using RoastedMarketplace.Infrastructure.Mvc.Models;

namespace RoastedMarketplace.Areas.Administration.Models.Shop
{
    public class ProductRelationModel : FoundationEntityModel
    {
        public bool IsReciprocal { get; set; }

        public ProductModel DestinationProduct { get; set; }
    }
}