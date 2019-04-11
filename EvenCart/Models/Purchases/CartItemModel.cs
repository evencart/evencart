using System.Collections.Generic;
using EvenCart.Infrastructure.Mvc.Models;
using EvenCart.Models.Products;

namespace EvenCart.Models.Purchases
{
    public class CartItemModel : FoundationModel
    {
        public int ProductId { get; set; }

        public string AttributeJson { get; set; }

        public decimal ComparePrice { get; set; } // = 100

        public decimal Price { get; set; } //= 80

        public int Quantity { get; set; }

        public decimal Tax { get; set; }

        public decimal TaxPercent { get; set; }

        public decimal Discount { get; set; } //=10

        public decimal FinalPrice { get; set; } //=70

        public bool IsWishlist { get; set; }

        public IList<ProductAttributeModel> Attributes { get; set; }
    }
}