using System;
using EvenCart.Data.Entity.Purchases;
using EvenCart.Infrastructure.Mvc.Models;
using EvenCart.Infrastructure.Mvc.Validator;
using FluentValidation;

namespace EvenCart.Areas.Administration.Models.Orders
{
    public class ShipmentHistoryModel : FoundationEntityModel, IRequiresValidations<ShipmentHistoryModel>
    {
        public int ShipmentId { get; set; }

        public ShipmentStatus ShipmentStatus { get; set; }

        public string ShipmentStatusDisplay => ShipmentStatus.ToString();

        public DateTime CreatedOn { get; set; }

        public void SetupValidationRules(ModelValidator<ShipmentHistoryModel> v)
        {
            v.RuleFor(x => x.ShipmentStatus).NotEmpty().When(x => x.Id < 1);
            v.RuleFor(x => x.CreatedOn).NotEmpty().When(x => x.Id > 0);
            v.RuleFor(x => x.ShipmentId).GreaterThan(0);
        }
    }
}