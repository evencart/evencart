using System.Collections.Generic;
using RoastedMarketplace.Core.Services;
using RoastedMarketplace.Data.Entity.Shop;
using RoastedMarketplace.Data.Entity.Users;

namespace RoastedMarketplace.Services.Users
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