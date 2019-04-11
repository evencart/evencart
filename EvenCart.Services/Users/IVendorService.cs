using System.Collections.Generic;
using EvenCart.Core.Services;
using EvenCart.Data.Entity.Shop;
using EvenCart.Data.Entity.Users;

namespace EvenCart.Services.Users
{
    public interface IVendorService : IFoundationEntityService<Vendor>
    {
        void AddVendorUser(int vendorId, int userId);

        void RemoveVendorUser(int vendorId, int userId);

        void AddVendorProduct(int vendorId, int productId);

        void RemoveVendorProduct(int vendorId, int productId);

        IList<Vendor> GetVendorsByProductIds(int[] productIds);

        IList<Vendor> GetVendors(string searchText, int page, int count, out int totalMatches);
    }
}