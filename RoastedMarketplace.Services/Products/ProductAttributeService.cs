using System.Linq;
using DotEntity;
using RoastedMarketplace.Core.Services;
using RoastedMarketplace.Data.Entity.Shop;

namespace RoastedMarketplace.Services.Products
{
    public class ProductAttributeService : FoundationEntityService<ProductAttribute>, IProductAttributeService
    {
        public override void Insert(ProductAttribute entity)
        {
            base.Insert(entity);

            //add other attributes
            foreach (var ppav in entity.ProductAttributeValues)
            {
                ppav.ProductAttributeId = entity.Id;
            }
            EntitySet<ProductAttributeValue>.Insert(entity.ProductAttributeValues.ToArray());
        }

        public void RemoveProductAttributeValue(int productAttributeValueId)
        {
            EntitySet<ProductAttributeValue>.Delete(x => x.Id == productAttributeValueId);
        }

        public void RemoveProductAttribute(int productAttributeId)
        {
            //delete in a transaction because of multiples
            using (var transaction = EntitySet.BeginTransaction())
            {
                EntitySet<ProductAttributeValue>.Delete(x => x.ProductAttributeId == productAttributeId, transaction);
                EntitySet<ProductAttribute>.Delete(x => x.Id == productAttributeId, transaction);
                transaction.Commit();
            }
        }

        public void AddProductAttributeValue(ProductAttributeValue productAttributeValue)
        {
            EntitySet<ProductAttributeValue>.Insert(productAttributeValue);
        }
    }
}