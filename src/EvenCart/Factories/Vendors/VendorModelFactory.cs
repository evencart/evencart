using EvenCart.Data.Entity.Users;
using EvenCart.Models.Vendors;

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