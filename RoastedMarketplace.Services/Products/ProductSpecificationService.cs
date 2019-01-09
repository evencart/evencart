using System.Collections.Generic;
using System.Linq;
using DotEntity;
using DotEntity.Enumerations;
using RoastedMarketplace.Core.Services;
using RoastedMarketplace.Data.Entity.Shop;
using RoastedMarketplace.Data.Extensions;

namespace RoastedMarketplace.Services.Products
{
    public class ProductSpecificationService : FoundationEntityService<ProductSpecification>, IProductSpecificationService
    {
        public IList<ProductSpecification> GetByProductId(int productId, int? groupId = null)
        {
            var query = Repository
                .Join<ProductSpecificationGroup>("ProductSpecificationGroupId", "Id", joinType: JoinType.LeftOuter)
                .Join<ProductSpecificationValue>("Id", "ProductSpecificationId", SourceColumn.Parent, JoinType.LeftOuter)
                .Join<AvailableAttributeValue>("AvailableAttributeValueId", "Id", joinType: JoinType.LeftOuter)
                .Join<AvailableAttribute>("AvailableAttributeId", "Id", joinType: JoinType.LeftOuter)
                .Relate(RelationTypes.OneToOne<ProductSpecification, ProductSpecificationGroup>())
                .Relate(RelationTypes.OneToMany<ProductSpecification, ProductSpecificationValue>())
                .Relate<AvailableAttributeValue>((specification, value) =>
                {
                    if (specification.Tag == null)
                        specification.Tag = new List<AvailableAttributeValue>();

                    var attributeList = (List<AvailableAttributeValue>)specification.Tag;
                    if (!attributeList.Contains(value))
                        attributeList.Add(value);

                    var pav = specification.ProductSpecificationValues.FirstOrDefault(
                        x => x.AvailableAttributeValueId == value.Id);
                    if (pav != null)
                        pav.AvailableAttributeValue = value;
                })
                .Relate<AvailableAttribute>((specification, availableAttribute) =>
                {
                    availableAttribute.AvailableAttributeValues = (List<AvailableAttributeValue>)specification.Tag;
                    specification.AvailableAttribute = availableAttribute;
                })
                .Where(x => x.ProductId == productId);

            if(groupId.HasValue)
                query = query.Where(x => x.ProductSpecificationGroupId == groupId);
            return query.SelectNested().ToList();
        }

        public override ProductSpecification Get(int id)
        {
            return Repository.Where(x => x.Id == id)
                .Join<ProductSpecificationValue>("Id", "ProductSpecificationId", joinType: JoinType.LeftOuter)
                .Join<AvailableAttributeValue>("AvailableAttributeValueId", "Id", joinType: JoinType.LeftOuter)
                .Join<AvailableAttribute>("AvailableAttributeId", "Id", joinType: JoinType.LeftOuter)
                .Relate(RelationTypes.OneToMany<ProductSpecification, ProductSpecificationValue>())
                .Relate<AvailableAttributeValue>((productSpecification, availableAttributeValue) =>
                {
                    if (productSpecification.Tag == null)
                        productSpecification.Tag = new List<AvailableAttributeValue>();

                    var attributeList = (List<AvailableAttributeValue>)productSpecification.Tag;
                    if (!attributeList.Contains(availableAttributeValue))
                        attributeList.Add(availableAttributeValue);

                    var pav = productSpecification.ProductSpecificationValues.FirstOrDefault(
                        x => x.AvailableAttributeValueId == availableAttributeValue.Id);
                    if (pav != null)
                        pav.AvailableAttributeValue = availableAttributeValue;
                })
                .Relate<AvailableAttribute>((productSpecification, availableAttribute) =>
                {
                    availableAttribute.AvailableAttributeValues = (List<AvailableAttributeValue>)productSpecification.Tag;
                    productSpecification.AvailableAttribute = availableAttribute;
                })
                .SelectNested()
                .FirstOrDefault();
        }
    }
}