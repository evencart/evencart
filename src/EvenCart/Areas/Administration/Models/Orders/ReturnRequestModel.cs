using EvenCart.Areas.Administration.Models.Users;
using EvenCart.Data.Entity.Purchases;
using EvenCart.Infrastructure.Mvc.Models;
using EvenCart.Infrastructure.Mvc.Validator;
using FluentValidation;

namespace EvenCart.Areas.Administration.Models.Orders
{
    public class ReturnRequestModel : FoundationEntityModel, IRequiresValidations<ReturnRequestModel>
    {
        public ReturnRequestStatus ReturnRequestStatus { get; set; }

        public ReturnOption ReturnOption { get; set; }

        public string ReturnOptionDisplay => ReturnOption.ToString();

        public string ProductName { get; set; }

        public int ProductId { get; set; }

        public string AttributeText { get; set; }

        public string ImageUrl { get; set; }

        public int Quantity { get; set; }

        public int OrderedQuantity { get; set; }

        public string ReturnReason { get; set; }

        public string ReturnAction { get; set; }

        public string OrderNumber { get; set; }

        public int OrderId { get; set; }

        public string CustomerComments { get; set; }

        public string StaffComments { get; set; }

        public string Remarks { get; set; }

        public int ReturnOrderId { get; set; }

        public string ReturnOrderNumber { get; set; }

        public UserModel User { get; set; }

        public void SetupValidationRules(ModelValidator<ReturnRequestModel> v)
        {
            v.RuleFor(x => x.Id).GreaterThan(0);
        }
    }
}