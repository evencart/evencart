#region License
// Copyright (c) Sojatia Infocrafts Private Limited.
// The following code is part of EvenCart eCommerce Software (https://evencart.co) Dual Licensed under the terms of
// 
// 1. GNU GPLv3 with additional terms (available to read at https://evencart.co/license)
// 2. EvenCart Proprietary License (available to read at https://evencart.co/license/commercial-license).
// 
// You can select one of the above two licenses according to your requirements. The usage of this code is
// subject to the terms of the license chosen by you.
#endregion

using Genesis.Infrastructure.Mvc.Models;

namespace EvenCart.Models.Orders
{
    public class OrderItemModel : GenesisEntityModel
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