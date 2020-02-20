using EvenCart.Infrastructure.Mvc.Models;

namespace EvenCart.Models.Orders
{
    public class OrderItemModel : FoundationEntityModel
    {
        /// <summary>
        /// The id of product
        /// </summary>
        public int ProductId { get; set; }
        /// <summary>
        /// The url of product image
        /// </summary>
        public string ImageUrl { get; set; }
        /// <summary>
        /// The name of the product
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// The seo slug of the product
        /// </summary>
        public string SeName { get; set; }
        /// <summary>
        /// The attributes representing the product variant
        /// </summary>
        public string AttributeText { get; set; }
        /// <summary>
        /// The price of the product
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// The total price of the product
        /// </summary>
        public decimal TotalPrice { get; set; }
        /// <summary>
        /// The quantity of the product ordered
        /// </summary>
        public int Quantity { get; set; }
        /// <summary>
        /// The tax amount specific to this product
        /// </summary>
        public decimal Tax { get; set; }
        /// <summary>
        /// The tax percent specific to this product
        /// </summary>
        public decimal TaxPercent { get; set; }
        /// <summary>
        /// The tax name applicable of this product
        /// </summary>
        public string TaxName { get; set; }
        /// <summary>
        /// Specifies if the product is downloadable
        /// </summary>
        public bool IsDownloadable { get; set; }
    }
}