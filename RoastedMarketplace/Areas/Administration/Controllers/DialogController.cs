using Microsoft.AspNetCore.Mvc;
using RoastedMarketplace.Areas.Administration.Models.Dialog;
using RoastedMarketplace.Infrastructure.Mvc;
using RoastedMarketplace.Infrastructure.Routing;

namespace RoastedMarketplace.Areas.Administration.Controllers
{
    public class DialogController : FoundationAdminController
    {
        [HttpGet("selector")]
        public IActionResult Selector([FromQuery] DialogModel dialogModel)
        {
            var model = new DialogResultModel() {
                MultiSelect = dialogModel.MultiSelect,
                ResponseObjectName = dialogModel.EntityName.ToLowerInvariant()
            };
            var url = "";
            var title = "";
            var displayField = "name";
            switch (model.ResponseObjectName)
            {
                case "categories":
                    url = Url.RouteUrl(AdminRouteNames.CategoriesList);
                    title = T("Categories");
                    displayField = "fullCategoryPath";
                    break;
                case "products":
                    url = Url.RouteUrl(AdminRouteNames.ProductsList);
                    title = T("Products");
                    break;
                case "users":
                    url = Url.RouteUrl(AdminRouteNames.UsersList);
                    title = T("Users");
                    break;
                case "roles":
                    url = Url.RouteUrl(AdminRouteNames.RolesList);
                    title = T("Roles");
                    break;
                case "vendors":
                    url = Url.RouteUrl(AdminRouteNames.VendorsList);
                    title = T("Vendors");
                    break;
                case "manufacturers":
                    url = Url.RouteUrl(AdminRouteNames.ManufacturersList);
                    title = T("Manufacturers");
                    break;
                case "paymentmethods":
                    url = Url.RouteUrl(AdminRouteNames.PaymentMethodsList);
                    title = T("Payment Methods");
                    break;
                case "shippingmethods":
                    url = Url.RouteUrl(AdminRouteNames.ShippingMethodsList);
                    title = T("Shipping Methods");
                    break;
                default:
                    return NotFound();
            }
            model.Url = url;
            model.DialogTitle = title;
            return R.Success.With("selector", dialogModel).Result;
        }
    }
}