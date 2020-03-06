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
using System.Linq;
using EvenCart.Data.Entity.Purchases;
using EvenCart.Data.Entity.Shop;
using EvenCart.Data.Extensions;

namespace EvenCart.Services.Products
{
    public class ProductAccountant : IProductAccountant
    {
        private readonly IProductVariantService _productVariantService;
        public ProductAccountant(IProductVariantService productVariantService)
        {
            _productVariantService = productVariantService;
        }

        public StockStatus GetStockStatus(Product product, IList<int> productAttributeValueIds, out ProductVariant variant)
        {
            variant = null;
            //return if we don't need to track
            if (!product.HasVariants && !product.TrackInventory)
                return StockStatus.InStock;

            //no variants but tracking
            if (!product.HasVariants && product.TrackInventory && product.Inventories.Any(x => x.AvailableQuantity > product.MinimumPurchaseQuantity))
                return StockStatus.InStock;
            
            //with variants
            if (product.HasVariants)
            {
                //get variant
                variant = _productVariantService.GetByAttributeValueIds(productAttributeValueIds);
                if(variant == null)
                    return StockStatus.Unavailable;
                
                if (!variant.TrackInventory)
                    return StockStatus.InStock;

                if (variant.TrackInventory && variant.IsAvailableInStock(product))
                    return StockStatus.InStock;
            }
            return StockStatus.OutOfStock;

        }

        public void UpdateStockStatusByOrder(Order order)
        {
           
        }
    }
}