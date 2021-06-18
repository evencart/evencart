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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DotEntity;
using DotEntity.Enumerations;
using Genesis.Services;
using EvenCart.Data.Entity.Shop;
using Genesis.Extensions;
using Genesis.Modules.Users;

namespace Genesis.Modules.Vendors
{
    public class VendorService : GenesisEntityService<Vendor>, IVendorService
    {
        public void AddVendorUser(int vendorId, int userId)
        {
            var vendorUser = EntitySet<VendorUser>.Where(x => x.UserId == userId && x.VendorId == vendorId)
                .SelectSingle();
            if (vendorUser != null)
                return;
            vendorUser = new VendorUser() {VendorId = vendorId, UserId = userId};
            EntitySet<VendorUser>.Insert(vendorUser);
        }

        public void RemoveVendorUser(int vendorId, int userId)
        {
            EntitySet<VendorUser>.Delete(x => x.UserId == userId && x.VendorId == vendorId);
        }

        public void AddVendorProduct(int vendorId, int productId)
        {
            var vendorProduct = EntitySet<ProductVendor>.Where(x => x.ProductId == productId && x.VendorId == vendorId)
                .SelectSingle();
            if (vendorProduct != null)
                return;
            vendorProduct = new ProductVendor() { VendorId = vendorId, ProductId = productId};
            EntitySet<ProductVendor>.Insert(vendorProduct);
        }

        public void RemoveVendorProduct(int vendorId, int productId)
        {
            EntitySet<ProductVendor>.Delete(x => x.ProductId == productId && x.VendorId == vendorId);
        }

        public ProductVendor GetVendorProduct(int vendorProductId)
        {
            return EntitySet<ProductVendor>.Where(x => x.Id == vendorProductId).SelectSingle();
        }

        public IList<Vendor> GetVendorsByProductIds(int[] productIds)
        {
            var idsAsList = productIds.ToList();
            Expression<Func<ProductVendor, bool>> where = vendor => idsAsList.Contains(vendor.ProductId);
            return Repository
                .Join<ProductVendor>("Id", "VendorId", joinType: JoinType.LeftOuter)
                .Where(where)
                .SelectNested()
                .ToList();
        }

        public IList<Vendor> GetVendors(out int totalMatches, string searchText, int? userId = null, VendorStatus? vendorStatus = null, int page = 1, int count = int.MaxValue)
        {
            var query = Repository;
            if (!searchText.IsNullEmptyOrWhiteSpace())
            {
                query = query.Where(x => x.Name.Contains(searchText) || x.GstNumber.Contains(searchText) ||
                                         x.Tin.Contains(searchText));
            }

            if (vendorStatus.HasValue)
            {
                query = query.Where(x => x.VendorStatus == vendorStatus);
            }
            query = query.OrderBy(x => x.Name);

            if (userId.HasValue && userId > 0)
            {
                Expression<Func<VendorUser, bool>> vendorWhere = user => user.UserId == userId;
                query.Join<VendorUser>("Id", "VendorId", joinType: JoinType.LeftOuter)
                    .Where(vendorWhere);
                return query.SelectNestedWithTotalMatches(out totalMatches, page, count).ToList();
            }
           
            return query.SelectWithTotalMatches(out totalMatches, page, count)
                .ToList();
        }

        public override Vendor Get(int id)
        {
            return Repository.Where(x => x.Id == id)
                .Join<VendorUser>("Id", "VendorId", joinType: JoinType.LeftOuter)
                .Join<User>("UserId", "Id", joinType: JoinType.LeftOuter)
                .Relate(RelationTypes.OneToMany<Vendor, User>())
                .SelectNested()
                .FirstOrDefault();
        }
    }
}