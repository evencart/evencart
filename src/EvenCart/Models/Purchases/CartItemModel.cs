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