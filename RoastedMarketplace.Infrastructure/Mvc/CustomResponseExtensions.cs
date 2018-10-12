using RoastedMarketplace.Infrastructure.Helpers;

namespace RoastedMarketplace.Infrastructure.Mvc
{
    public static class CustomResponseExtensions
    {
        public static FoundationController.CustomResponse WithGridResponse(this FoundationController.CustomResponse customResponse, int totalMatches, int currentPage, int count)
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
        public static FoundationController.CustomResponse WithAvailableCountries(this FoundationController.CustomResponse customResponse)
        {
            return customResponse.With("availableCountries", SelectListHelper.GetCountries());
        }

        /// <summary>
        /// Adds available address types to current response
        /// </summary>
        /// <param name="customResponse">The response object</param>
        /// <returns></returns>
        public static FoundationController.CustomResponse WithAvailableAddressTypes(this FoundationController.CustomResponse customResponse)
        {
            return customResponse.With("availableAddressTypes", SelectListHelper.GetAddressTypes());
        }

        /// <summary>
        /// Adds available input types to current response
        /// </summary>
        /// <param name="customResponse">The response object</param>
        /// <returns></returns>
        public static FoundationController.CustomResponse WithAvailableInputTypes(this FoundationController.CustomResponse customResponse)
        {
            return customResponse.With("inputTypes", SelectListHelper.GetInputTypes());
        }

    }
}