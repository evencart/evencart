using System.Collections.Generic;
using System.Linq;
using EvenCart.Infrastructure.Mvc.Models;
using EvenCart.Infrastructure.Mvc.Validator;
using FluentValidation;

namespace EvenCart.Areas.Administration.Models.Shop
{
    public class AvailableAttributeModel : FoundationEntityModel, IRequiresValidations<AvailableAttributeModel>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public IList<AvailableAttributeValueModel> AttributeValues { get; set; }

        public void SetupValidationRules(ModelValidator<AvailableAttributeModel> v)
        {
            v.RuleFor(x => x.Name).NotEmpty();
            v.RuleFor(x => x.AttributeValues).Must(x => x.Any());
        }
    }
}