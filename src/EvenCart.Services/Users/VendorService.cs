using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DotEntity;
using DotEntity.Enumerations;
using EvenCart.Core.Services;
using EvenCart.Data.Entity.Shop;
using EvenCart.Data.Entity.Users;
using EvenCart.Data.Extensions;

namespace EvenCart.Services.Users
{
    public class VendorService : FoundationEntityService<Vendor>, IVendorService
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

        public IList<Vendor> GetVendorsByProductIds(int[] productIds)
        {
            Expression<Func<ProductVendor, bool>> where = vendor => productIds.Contains(vendor.ProductId);
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