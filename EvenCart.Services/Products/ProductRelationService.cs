using System.Collections.Generic;
using System.Linq;
using EvenCart.Core.Services;
using EvenCart.Data.Entity.Shop;
using EvenCart.Data.Enum;

namespace EvenCart.Services.Products
{
    public class ProductRelationService : FoundationEntityService<ProductRelation>, IProductRelationService
    {
        private readonly IProductService _productService;
        public ProductRelationService(IProductService productService)
        {
            _productService = productService;
        }

        public IList<ProductRelation> GetByProductId(int productId, ProductRelationType? relationType, int page = 1, int count = 15)
        {
            var query = Repository.Where(x => x.SourceProductId == productId);
            if(relationType.HasValue)
                query = query.Where(x => x.RelationType == relationType);
            query = query.OrderBy(x => x.Id);
            //find the target products
            var productRelations = query.Select(page, count).ToList();
            if (!productRelations.Any())
                return productRelations;
            var targetProductIds = productRelations.Select(x => x.DestinationProductId).ToList();
            //now get their details
            var products = _productService.GetProducts(targetProductIds, true);
            foreach (var relation in productRelations)
                relation.DestinationProduct = products.FirstOrDefault(x => x.Id == relation.DestinationProductId);

            return productRelations;
        }

        public void RelateProducts(int productId, IList<int> destinationProductIds, ProductRelationType relationType, bool isReciprocal)
        {
            //get already saved related products
            var sourceProductIds = new List<int>() {productId};
            if (isReciprocal)
            {
                sourceProductIds = sourceProductIds.Concat(destinationProductIds).ToList();
            }
            var savedRelations = Repository.Where(x => sourceProductIds.Contains(x.SourceProductId) && x.RelationType == relationType)
                .Select()
                .ToList();

            // find the ones to be added
            var toAddIds = destinationProductIds.Except(savedRelations.Select(x => x.DestinationProductId).ToList());
            Transaction.Initiate(transaction =>
            {
                foreach (var id in toAddIds)
                {
                    var relation = new ProductRelation() {
                        SourceProductId = productId,
                        DestinationProductId = id,
                        RelationType = relationType
                    };
                    Insert(relation, transaction);

                    if (isReciprocal)
                    {
                        var reciprocalRelation = new ProductRelation() {
                            SourceProductId = id,
                            DestinationProductId = productId,
                            RelationType = relationType
                        };
                        Insert(reciprocalRelation, transaction);
                    }

                }

              
            });

        }
    }
}