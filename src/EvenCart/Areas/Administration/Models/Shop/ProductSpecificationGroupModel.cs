using EvenCart.Infrastructure.Mvc.Models;
using EvenCart.Infrastructure.Mvc.Validator;
using FluentValidation;

namespace EvenCart.Areas.Administration.Models.Shop
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
            if (obj is ProductSpecificationGroupModel grpModel)
            {
                return this.Name == grpModel.Name && this.ProductId == grpModel.ProductId;
            }

            return false;
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