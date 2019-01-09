using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DotEntity;
using DotEntity.Enumerations;
using RoastedMarketplace.Core.Services;
using RoastedMarketplace.Data.Entity.Shop;
using RoastedMarketplace.Data.Extensions;
using RoastedMarketplace.Services.Helpers;

namespace RoastedMarketplace.Services.Products
{
    public class ProductAttributeService : FoundationEntityService<ProductAttribute>, IProductAttributeService
    {
        private readonly IProductAttributeValueService _productAttributeValueService;
        public ProductAttributeService(IProductAttributeValueService productAttributeValueService)
        {
            _productAttributeValueService = productAttributeValueService;
        }

        public override void Insert(ProductAttribute entity, Transaction transaction = null)
        {
            base.Insert(entity, transaction);

            if (entity.ProductAttributeValues != null)
            {
                //add other attributes
                foreach (var ppav in entity.ProductAttributeValues)
                {
                    ppav.ProductAttributeId = entity.Id;
                }
                _productAttributeValueService.Insert(entity.ProductAttributeValues.ToArray());
            }
        }

        public void RemoveProductAttributeValue(int productAttributeValueId)
        {
            _productAttributeValueService.Delete(x => x.Id == productAttributeValueId);
        }

        public void RemoveProductAttribute(int productAttributeId)
        {
            //delete in a transaction because of multiples
            Transaction.Initiate(transaction =>
            {
                _productAttributeValueService.Delete(x => x.ProductAttributeId == productAttributeId, transaction);
                Delete(x => x.Id == productAttributeId, transaction);
            });
        }

        public void AddProductAttributeValue(ProductAttributeValue productAttributeValue, Transaction transaction = null)
        {
            _productAttributeValueService.Insert(productAttributeValue, transaction);
        }

        public override ProductAttribute Get(int id)
        {
            return Repository.Where(x => x.Id == id)
                .Join<ProductAttributeValue>("Id", "ProductAttributeId", joinType: JoinType.LeftOuter)
                .Join<AvailableAttributeValue>("AvailableAttributeValueId", "Id", joinType: JoinType.LeftOuter)
                .Join<AvailableAttribute>("AvailableAttributeId", "Id", joinType: JoinType.LeftOuter)
                .Relate(RelationTypes.OneToMany<ProductAttribute, ProductAttributeValue>())
                .Relate<AvailableAttributeValue>((productAttribute, availableAttributeValue) =>
                {
                    if (productAttribute.Tag == null)
                        productAttribute.Tag = new List<AvailableAttributeValue>();

                    var attributeList = (List<AvailableAttributeValue>) productAttribute.Tag;
                    if (!attributeList.Contains(availableAttributeValue))
                        attributeList.Add(availableAttributeValue);

                    var pav = productAttribute.ProductAttributeValues.FirstOrDefault(
                        x => x.AvailableAttributeValueId == availableAttributeValue.Id);
                    if (pav != null)
                        pav.AvailableAttributeValue = availableAttributeValue;
                })
                .Relate<AvailableAttribute>((productAttribute, availableAttribute) =>
                {
                    availableAttribute.AvailableAttributeValues = (List<AvailableAttributeValue>) productAttribute.Tag;
                    productAttribute.AvailableAttribute = availableAttribute;
                })
                .SelectNested()
                .FirstOrDefault();
        }

        public IList<ProductAttribute> GetByProductId(int productId, bool onlyVariantSpecific = false)
        {

            var q = Repository.Where(x => x.ProductId == productId)
                //.Join<Product>("ProductId", "Id")
                .Join<ProductAttributeValue>("Id", "ProductAttributeId", SourceColumn.Parent, joinType: JoinType.LeftOuter)
                .Join<AvailableAttributeValue>("AvailableAttributeValueId", "Id", joinType: JoinType.LeftOuter)
                .Join<AvailableAttribute>("AvailableAttributeId", "Id", joinType: JoinType.LeftOuter);

            //should we restrict to only variant specific attributes
            if (onlyVariantSpecific)
            {
                var allowedInputTypes = VariantHelper.GetVariantSpecificFieldTypes();
                Expression<Func<ProductAttribute, bool>> variantWhere =
                    attribute => allowedInputTypes.Contains(attribute.InputFieldType);
                q.Where(variantWhere);
            }


            return q
                .Relate(RelationTypes.OneToMany<ProductAttribute, ProductAttributeValue>())
                .Relate<AvailableAttributeValue>((productAttribute, availableAttributeValue) =>
                {
                    if (productAttribute.Tag == null)
                        productAttribute.Tag = new List<AvailableAttributeValue>();

                    var attributeList = (List<AvailableAttributeValue>) productAttribute.Tag;
                    if (!attributeList.Contains(availableAttributeValue))
                        attributeList.Add(availableAttributeValue);

                    var pav = productAttribute.ProductAttributeValues.FirstOrDefault(
                        x => x.AvailableAttributeValueId == availableAttributeValue.Id);
                    if (pav != null)
                        pav.AvailableAttributeValue = availableAttributeValue;
                })
                .Relate<AvailableAttribute>((productAttribute, availableAttribute) =>
                {
                    availableAttribute.AvailableAttributeValues = (List<AvailableAttributeValue>) productAttribute.Tag;
                    productAttribute.AvailableAttribute = availableAttribute;
                })
                .SelectNested()
                .ToList();
        }
    }
}