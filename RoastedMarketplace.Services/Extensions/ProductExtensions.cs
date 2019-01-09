using RoastedMarketplace.Data.Entity.Shop;

namespace RoastedMarketplace.Services.Extensions
{
    public static class ProductExtensions
    {
        /// <summary>
        /// Checks if a product is available in public store
        /// </summary>
        public static bool IsPublic(this Product product)
        {
            return product != null && product.Published && !product.Deleted;
        }
    }
}