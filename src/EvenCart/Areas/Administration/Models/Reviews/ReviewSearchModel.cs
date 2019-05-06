using EvenCart.Infrastructure.Mvc.Models;

namespace EvenCart.Areas.Administration.Models.Reviews
{
    public class ReviewSearchModel : AdminSearchModel
    {
        public bool? Published { get; set; }

        public int ProductId { get; set; }

        public string ProductSearch { get; set; }
    }
}