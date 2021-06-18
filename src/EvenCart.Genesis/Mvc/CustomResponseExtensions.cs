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

using EvenCart.Data.Entity.Shop;
using EvenCart.Data.Enum;
using EvenCart.Genesis.Helpers;
using Genesis.Infrastructure.Mvc;
using GenesisSelectListHelper = Genesis.Helpers.SelectListHelper;
namespace EvenCart.Genesis.Mvc
{
    public static class CustomResponseExtensions
    {
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

        public static CustomResponse WithCatalogPaginationTypes(this CustomResponse customResponse)
        {
            return customResponse.With("catalogPaginationTypes", GenesisSelectListHelper.GetSelectItemList<CatalogPaginationType>());
        }

        public static CustomResponse WithEmailAccounts(this CustomResponse customResponse)
        {
            return customResponse.With("emailAccounts", SelectListHelper.GetAvailableEmailAccounts());
        }

        public static CustomResponse WithProductSaleTypes(this CustomResponse customResponse)
        {
            return customResponse.With("productSaleTypes", GenesisSelectListHelper.GetSelectItemList<ProductSaleType>());
        }

        public static CustomResponse WithProductTypes(this CustomResponse customResponse)
        {
            return customResponse.With("productTypes", GenesisSelectListHelper.GetSelectItemList<ProductType>());
        }
    }
}