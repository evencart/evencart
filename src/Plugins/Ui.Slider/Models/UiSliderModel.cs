using EvenCart.Infrastructure.Mvc.Models;
using EvenCart.Infrastructure.Mvc.Validator;
using FluentValidation;

namespace Ui.Slider.Models
{
    public class UiSliderModel : FoundationEntityModel, IRequiresValidations<UiSliderModel>
    {
        public string Title { get; set; }

        public int MediaId { get; set; }

        public int DisplayOrder { get; set; }

        public bool Visible { get; set; }

        public string ImageUrl { get; set; }

        public string Url { get; set; }

        public void SetupValidationRules(ModelValidator<UiSliderModel> v)
        {
            v.RuleFor(x => x.MediaId).GreaterThan(0);
            v.RuleFor(x => x.Id).GreaterThanOrEqualTo(0);
        }
    }
}