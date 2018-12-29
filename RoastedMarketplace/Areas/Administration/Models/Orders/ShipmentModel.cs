using System.Collections.Generic;
using FluentValidation;
using Microsoft.AspNetCore.Mvc.Rendering;
using RoastedMarketplace.Data.Entity.Purchases;
using RoastedMarketplace.Infrastructure.Helpers;
using RoastedMarketplace.Infrastructure.Mvc.Models;
using RoastedMarketplace.Infrastructure.Mvc.Validator;

namespace RoastedMarketplace.Areas.Administration.Models.Orders
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

        public IList<SelectListItem> AvailableShipmentStatus { get; } =
            SelectListHelper.GetSelectItemList<ShipmentStatus>();

        public void SetupValidationRules(ModelValidator<ShipmentModel> v)
        {
            v.RuleFor(x => x.OrderId).GreaterThan(0);
        }
    }
}