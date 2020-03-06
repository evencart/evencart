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
using EvenCart.Core.Services;
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

        IList<Vendor> GetVendors(out int totalMatches, string searchText, int? userId = null, VendorStatus? vendorStatus = null, int page = 1, int count = int.MaxValue);
    }
}