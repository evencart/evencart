using System.Collections.Generic;
using EvenCart.Data.Entity.Purchases;
using EvenCart.Infrastructure.Mvc.Models;
using EvenCart.Infrastructure.Mvc.Validator;
using FluentValidation;

namespace EvenCart.Areas.Administration.Models.Orders
{
    public class ShipmentModel : FoundationEntityModel, IRequiresValidations<ShipmentModel>
    {
        public int OrderId { get; set; }

        public string TrackingNumber { get; set; }

        public string Remarks { get; set; }

        public string ShippingMethodName { get; set; }

        public ShipmentStatus ShipmentStatus { get; set; }

        public string ShipmentStatusDisplay => ShipmentStatus.ToString();

        public int ShipmentStatusId => (int) ShipmentStatus;

        public IList<ShipmentItemModel> ShipmentItems { get; set; }

        public IList<ShipmentHistoryModel> ShipmentHistoryItems { get; set; }

        public int WarehouseId { get; set; }

        public string ShippingLabelUrl { get; set; }

        public string TrackingUrl { get; set; }

        public bool SupportsLabelPurchase { get; set; }

        public void SetupValidationRules(ModelValidator<ShipmentModel> v)
        {
            v.RuleFor(x => x.OrderId).GreaterThan(0);
            v.RuleFor(x => x.WarehouseId).GreaterThan(0);
        }
    }
}