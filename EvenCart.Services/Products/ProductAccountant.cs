using System.Collections.Generic;
using EvenCart.Data.Entity.Shop;

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
            if (!product.HasVariants && product.TrackInventory && product.StockQuantity > 0)
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

                if (variant.TrackInventory && variant.StockQuantity > 0)
                    return StockStatus.InStock;
            }
            return StockStatus.OutOfStock;

        }
    }
}