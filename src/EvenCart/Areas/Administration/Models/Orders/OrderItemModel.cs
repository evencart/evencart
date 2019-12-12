using EvenCart.Infrastructure.Mvc.Attributes;
using EvenCart.Infrastructure.Mvc.Models;

namespace EvenCart.Areas.Administration.Models.Orders
{
    [FormatAsCurrencies(nameof(Price), nameof(TotalPrice), nameof(LineTotal), nameof(Tax))]
    public class OrderItemModel : FoundationEntityModel
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public string ImageUrl { get; set; }

        public string AttributeText { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public decimal TotalPrice { get; set; }

        public decimal LineTotal => TotalPrice + Tax;

        public decimal Tax { get; set; }

        public decimal TaxPercent { get; set; }

        public bool Shipped { get; set; }
    }
}