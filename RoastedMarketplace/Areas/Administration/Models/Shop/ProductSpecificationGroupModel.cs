using FluentValidation;
using RoastedMarketplace.Infrastructure.Mvc.Models;
using RoastedMarketplace.Infrastructure.Mvc.Validator;

namespace RoastedMarketplace.Areas.Administration.Models.Shop
{
    public class ProductSpecificationGroupModel : FoundationEntityModel, IRequiresValidations<ProductSpecificationGroupModel>
    {
        public string Name { get; set; }

        public int DisplayOrder { get; set; }

        public int ProductId { get; set; }

        public void SetupValidationRules(ModelValidator<ProductSpecificationGroupModel> v)
        {
            v.RuleFor(x => x.Name).NotEmpty();
        }

        public override bool Equals(object obj)
        {
            var grpModel = (ProductSpecificationGroupModel) obj;
            return this.Name == grpModel.Name && this.ProductId == grpModel.ProductId;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                // Maybe nullity checks, if these are objects not primitives!
                hash = hash * 23 + Name.GetHashCode();
                hash = hash * 23 + ProductId.GetHashCode();
                return hash;
            }
        }
    }
}