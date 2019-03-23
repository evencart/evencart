using RoastedMarketplace.Data.Entity.Purchases;
using RoastedMarketplace.Infrastructure.Mvc.Models;
using RoastedMarketplace.Models.Products;

namespace RoastedMarketplace.Models.Reviews
{
    public class PendingReviewModel : FoundationModel
    {
        public string OrderNumber { get; set; }

        public string OrderGuid { get; set; }

        public OrderStatus OrderStatus { get; set; }

        public ProductModel Product { get; set; }
    }
}