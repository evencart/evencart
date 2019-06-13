using EvenCart.Infrastructure.Mvc.Models;
using EvenCart.Infrastructure.Mvc.Validator;
using FluentValidation;

namespace EvenCart.Areas.Administration.Models.Reports
{
    public class StockReportSearchModel : AdminSearchModel, IRequiresValidations<StockReportSearchModel>
    {
        public string ProductSearch { get; set; }

        public bool? Published { get; set; }

        public int WarehouseId { get; set; }

        public void SetupValidationRules(ModelValidator<StockReportSearchModel> v)
        {
            v.RuleFor(x => x.WarehouseId).GreaterThan(0);
        }
    }
}