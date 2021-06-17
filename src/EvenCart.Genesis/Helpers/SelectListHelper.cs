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

using System.Collections.Generic;
using System.Linq;
using EvenCart.Data.Entity.Payments;
using EvenCart.Data.Entity.Purchases;
using Genesis;
using Genesis.Modules.Addresses;
using Genesis.Modules.Emails;
using Microsoft.AspNetCore.Mvc.Rendering;
using GenesisSelectListHelper = Genesis.Helpers.SelectListHelper;
namespace EvenCart.Genesis.Helpers
{
    public static class SelectListHelper
    {
        public static List<SelectListItem> GetCountries()
        {
            var countryService = D.Resolve<ICountryService>();
            var countries = countryService.Get(x => x.Published).ToList();
            return GenesisSelectListHelper.GetSelectItemList(countries, country => country.Id, country => country.Name);
        }

        public static List<SelectListItem> GetStatesOrProvinces(int countryId)
        {
            var stateProvinceService = D.Resolve<IStateOrProvinceService>();
            var states = stateProvinceService.Get(x => x.CountryId == countryId).ToList();
            return GenesisSelectListHelper.GetSelectItemList(states, state => state.Id, state => state.Name);
        }

        public static List<SelectListItem> GetAddressTypes()
        {
            return GenesisSelectListHelper.GetSelectItemList<AddressType>();
        }

        public static List<SelectListItem> GetPaymentStatusItems()
        {
            return GenesisSelectListHelper.GetSelectItemList<PaymentStatus>();
        }

        public static List<SelectListItem> GetOrderStatusItems()
        {
            return GenesisSelectListHelper.GetSelectItemList<OrderStatus>();
        }

        public static List<SelectListItem> GetShipmentStatusItems()
        {
            return GenesisSelectListHelper.GetSelectItemList<ShipmentStatus>();
        }

        public static List<SelectListItem> GetAvailableEmailAccounts()
        {
            var emailAccountService = D.Resolve<IEmailAccountService>();
            var emailAccounts = emailAccountService.Get(x => true).ToList();
            return GenesisSelectListHelper.GetSelectItemList(emailAccounts, account => account.Id, account => account.Email);
        }
    }
}