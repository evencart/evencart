using System.Collections.Generic;
using EvenCart.Infrastructure.Mvc.Models;

namespace EvenCart.Models.Components
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