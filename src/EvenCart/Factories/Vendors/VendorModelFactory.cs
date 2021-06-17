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

using EvenCart.Models.Vendors;
using Genesis.Modules.Vendors;

namespace EvenCart.Factories.Vendors
{
    public class VendorModelFactory : IVendorModelFactory
    {
        public VendorModel Create(Vendor vendor)
        {
            var vendorModel = new VendorModel
            {
                Address = vendor.Address,
                City = vendor.City,
                Email = vendor.Email,
                Name = vendor.Name,
                GstNumber = vendor.GstNumber,
                CountryId = vendor.CountryId,
                Pan = vendor.Pan,
                StateProvinceId = vendor.StateProvinceId,
                StateProvinceName = vendor.StateProvinceName,
                Tin = vendor.Tin,
                ZipPostalCode = vendor.ZipPostalCode,
                Phone = vendor.Phone,
                VendorStatus = vendor.VendorStatus,
                Id = vendor.Id
            };
            return vendorModel;
        }
    }
}