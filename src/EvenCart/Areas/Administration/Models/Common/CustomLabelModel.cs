using EvenCart.Data.Entity.Common;
using EvenCart.Infrastructure.Mvc.Models;
using EvenCart.Infrastructure.Mvc.Validator;
using FluentValidation;

namespace EvenCart.Areas.Administration.Models.Common
{
    /// <summary>
    /// Represents a custom label
    /// </summary>
    public class CustomLabelModel : FoundationEntityModel, IRequiresValidations<CustomLabelModel>
    {
        /// <summary>
        /// The text of the label
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// The <see cref="CustomLabelType">label type</see>
        /// </summary>
        public string LabelType { get; set; }

        /// <summary>
        /// The display order of the label
        /// </summary>
        public int DisplayOrder { get; set; }

        public void SetupValidationRules(ModelValidator<CustomLabelModel> v)
        {
            v.RuleFor(x => x.Text).NotEmpty();
            v.RuleFor(x => x.LabelType).NotEmpty();
        }
    }
}