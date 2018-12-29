using System.Collections.Generic;
using RoastedMarketplace.Infrastructure.Mvc.Models;

namespace RoastedMarketplace.Models.Components
{
    public class CategoryFilterModel : FoundationEntityModel
    {
        public string Name { get; set; }

        public string Url { get; set; }

        public bool Selected { get; set; }

        public int ParentCategoryId { get; set; }

        public List<CategoryFilterModel> ChildCategories { get; set; } = new List<CategoryFilterModel>();
       
    }
}