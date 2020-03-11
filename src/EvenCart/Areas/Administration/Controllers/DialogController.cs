#region License
// Copyright (c) Sojatia Infocrafts Private Limited.
// The following code is part of EvenCart eCommerce Software (https://evencart.co) Dual Licensed under the terms of
// 
// 1. GNU GPLv3 with additional terms (available to read at https://evencart.co/license)
// 2. EvenCart Proprietary License (available to read at https://evencart.co/license/commercial-license).
// 
// You can select one of the above two licenses according to your requirements. The usage of this code is
// subject to the terms of the license chosen by you.
#endregion

using EvenCart.Areas.Administration.Models.Dialog;
using EvenCart.Data.Extensions;
using EvenCart.Infrastructure;
using EvenCart.Infrastructure.Mvc;
using EvenCart.Infrastructure.Routing;
using Microsoft.AspNetCore.Mvc;

namespace EvenCart.Areas.Administration.Controllers
{
    public class DialogController : FoundationAdminController
    {
        [HttpGet("selector", Name = AdminRouteNames.SelectorDialog)]
        public IActionResult Selector([FromQuery] DialogModel dialogModel)
        {
            var model = new DialogResultModel() {
                MultiSelect = dialogModel.MultiSelect,
                ResponseObjectName = dialogModel.EntityName.ToCamelCase()
            };
            var url = "";
            var title = "";
            var displayField = "name";
            var routeName = "";
            switch (model.ResponseObjectName)
            {
                case "categories":
                    routeName = AdminRouteNames.CategoriesList;
                    title = T("Categories");
                    displayField = "fullCategoryPath";
                    break;
                case "products":
                    routeName = AdminRouteNames.ProductsList;
                    title = T("Products");
                    break;
                case "users":
                    routeName = AdminRouteNames.UsersList;
                    title = T("Users");
                    break;
                case "roles":
                    routeName = AdminRouteNames.RolesList;
                    title = T("Roles");
                    break;
                case "vendors":
                    routeName = AdminRouteNames.VendorsList;
                    title = T("Vendors");
                    break;
                case "manufacturers":
                    routeName = AdminRouteNames.ManufacturersList;
                    title = T("Manufacturers");
                    break;
                case "paymentMethods":
                    routeName = AdminRouteNames.PaymentMethodsList;
                    title = T("Payment Methods");
                    break;
                case "shippingMethods":
                    routeName = AdminRouteNames.ShippingMethodsList;
                    title = T("Shipping Methods");
                    break;
                default:
                    return NotFound();
            }
            model.ApiUrl = Url.RouteUrl($"{ApplicationConfig.ApiEndpointName}_{routeName}");
            model.UiUrl = Url.RouteUrl($"{routeName}");
            model.DialogTitle = title;
            model.DisplayField = displayField;
            return R.Success.With("selector", model).With("type", model.ResponseObjectName).Result;
        }
    }
}