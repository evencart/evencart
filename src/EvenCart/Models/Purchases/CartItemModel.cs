using System.Collections.Generic;
using EvenCart.Infrastructure.Mvc.Models;
using EvenCart.Models.Products;

namespace EvenCart.Models.Purchases
{
    public class CartItemModel : FoundationModel
    {
        /// <summary>
        /// The id of product to be added
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// The list of product attributes as json.  Ignored for POST requests.
        /// </summary>
        public string AttributeJson { get; set; }

        /// <summary>
        /// The price that'll be used for comparison. Ignored for POST requests.
        /// </summary>
        public decimal ComparePrice { get; set; } // = 100

        /// <summary>
        /// The price of the product
        /// </summary>
        public decimal Price { get; set; } //= 80

        /// <summary>
        /// The quantity that needs to be added to the cart.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// The calculated tax for the product. Ignored for POST requests.
        /// </summary>
        public decimal Tax { get; set; }

        /// <summary>
        /// The tax percent for product.  Ignored for POST requests.
        /// </summary>
        public decimal TaxPercent { get; set; }

        /// <summary>
        /// The discount for the product.  Ignored for POST requests.
        /// </summary>
        public decimal Discount { get; set; } //=10

        /// <summary>
        /// The final price of the product.  Ignored for POST requests.
        /// </summary>
        public decimal FinalPrice { get; set; } //=70

        /// <summary>
        /// Set to true if product should be added to wishlist instead of cart.
        /// </summary>
        public bool IsWishlist { get; set; }

        /// <summary>
        /// A collection of <see cref="ProductAttributeModel">attributes</see> that identify a product variant
        /// </summary>
        public IList<ProductAttributeModel> Attributes { get; set; }
    }
}