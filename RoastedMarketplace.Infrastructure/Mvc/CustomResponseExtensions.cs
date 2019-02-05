using RoastedMarketplace.Core.Infrastructure;
using RoastedMarketplace.Data.Enum;
using RoastedMarketplace.Infrastructure.Extensions;
using RoastedMarketplace.Infrastructure.Helpers;
using RoastedMarketplace.Infrastructure.ViewEngines;

namespace RoastedMarketplace.Infrastructure.Mvc
{
    public static class CustomResponseExtensions
    {
        public static CustomResponse WithGridResponse(this CustomResponse customResponse, int totalMatches, int currentPage, int count)
        {
            return customResponse.With("current", currentPage)
                .With("rowCount", count)
                .With("total", totalMatches);
        }
        /// <summary>
        /// Adds available countries to current response
        /// </summary>
        /// <param name="customResponse">The response object</param>
        /// <returns></returns>
        public static CustomResponse WithAvailableCountries(this CustomResponse customResponse)
        {
            return customResponse.With("availableCountries", SelectListHelper.GetCountries());
        }

        /// <summary>
        /// Adds available address types to current response
        /// </summary>
        /// <param name="customResponse">The response object</param>
        /// <returns></returns>
        public static CustomResponse WithAvailableAddressTypes(this CustomResponse customResponse)
        {
            return customResponse.With("availableAddressTypes", SelectListHelper.GetAddressTypes());
        }

        /// <summary>
        /// Adds available input types to current response
        /// </summary>
        /// <param name="customResponse">The response object</param>
        /// <returns></returns>
        public static CustomResponse WithAvailableInputTypes(this CustomResponse customResponse)
        {
            return customResponse.With("inputTypes", SelectListHelper.GetInputTypes());
        }

        /// <summary>
        /// Adds available input types to current response
        /// </summary>
        /// <param name="customResponse">The response object</param>
        /// <param name="paramsModel"></param>
        /// <returns></returns>
        public static CustomResponse WithParams(this CustomResponse customResponse, object paramsModel)
        {
            return customResponse.With("params", paramsModel);
        }

        public static CustomResponse WithAvailableShipmentStatusTypes(this CustomResponse customResponse)
        {
            return customResponse.With("shipmentStatusTypes", SelectListHelper.GetShipmentStatusItems());
        }

        public static CustomResponse WithAvailableOrderStatusTypes(this CustomResponse customResponse)
        {
            return customResponse.With("orderStatusTypes", SelectListHelper.GetOrderStatusItems());
        }

        public static CustomResponse WithAvailablePaymentStatusTypes(this CustomResponse customResponse)
        {
            return customResponse.With("paymentStatusTypes", SelectListHelper.GetPaymentStatusItems());
        }

        public static CustomResponse WithRawView(this CustomResponse customResponse, string viewPath)
        {
            var viewAccountant = DependencyResolver.Resolve<IViewAccountant>();
            var cachedView = viewAccountant.GetView(viewAccountant.GetThemeViewPath(viewPath), viewPath,
                ApplicationEngine.IsAdmin() ? ApplicationConfig.AdminAreaName : "");
            return customResponse.With("rawView", cachedView.ToSplited());
        }

        public static CustomResponse WithSeoMeta(this CustomResponse customResponse, object seoMeta)
        {
            return customResponse.With("seoMeta", seoMeta);
        }

        public static CustomResponse WithTimezones(this CustomResponse customResponse)
        {
            return customResponse.With("timezones", SelectListHelper.GetTimezones());
        }

        public static CustomResponse WithRegistrationModes(this CustomResponse customResponse)
        {
            return customResponse.With("registrationModes", SelectListHelper.GetSelectItemList<RegistrationMode>());
        }

        public static CustomResponse WithCatalogPaginationTypes(this CustomResponse customResponse)
        {
            return customResponse.With("catalogPaginationTypes", SelectListHelper.GetSelectItemList<CatalogPaginationType>());
        }

        public static CustomResponse WithEmailAccounts(this CustomResponse customResponse)
        {
            return customResponse.With("emailAccounts", SelectListHelper.GetAvailableEmailAccounts());
        }
    }
}