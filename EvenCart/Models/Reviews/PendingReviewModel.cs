using EvenCart.Data.Entity.Purchases;
using EvenCart.Infrastructure.Mvc.Models;
using EvenCart.Models.Products;

namespace EvenCart.Models.Reviews
{
    public class PendingReviewModel : FoundationModel
    {
        public string OrderNumber { get; set; }

        public string OrderGuid { get; set; }

        public OrderStatus OrderStatus { get; set; }

        public ProductModel Product { get; set; }
    }
}