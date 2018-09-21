using RoastedMarketplace.Infrastructure.Mvc.Models;

namespace RoastedMarketplace.Areas.Administration.Models.Shop
{
    public class CategoryModel : FoundationEntityModel
    {
        public string FullCategoryPath { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int DisplayOrder { get; set; }

        public string ImageUrl { get; set; }
    }
}