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
using EvenCart.Core.Services;
using EvenCart.Data.Entity.Promotions;
using EvenCart.Data.Extensions;

namespace EvenCart.Services.Promotions
{
    public class DiscountCouponService : FoundationEntityService<DiscountCoupon>, IDiscountCouponService
    {
        private readonly IRestrictionValueService _restrictionValueService;
        public DiscountCouponService(IRestrictionValueService restrictionValueService)
        {
            _restrictionValueService = restrictionValueService;
        }

        public override DiscountCoupon Get(int id)
        {
            return GetByWhere(x => x.Id == id)
                .SelectNested()
                .FirstOrDefault();
        }

        public override IEnumerable<DiscountCoupon> Get(Expression<Func<DiscountCoupon, bool>> @where, int page = 1, int count = Int32.MaxValue)
        {
            return GetByWhere(where)
                .SelectNested(page, count);
        }

        public DiscountCoupon GetByCouponCode(string couponCode)
        {
            couponCode = couponCode.ToLower();
            return GetByWhere(x => x.CouponCode == couponCode && x.Enabled)
                .OrderBy(x => x.Id, RowOrder.Descending)
                .SelectNested(1, 1)
                .FirstOrDefault(); ;
        }

        public IEnumerable<DiscountCoupon> SearchDiscountCoupons(string searchText, out int totalMatches, int page = 1, int count = 15)
        {
            var query = Repository;
            if (!searchText.IsNullEmptyOrWhiteSpace())
                query = query.Where(x => x.Name.Contains(searchText));
            query = query.OrderBy(x => x.Name);
            return query.SelectWithTotalMatches(out totalMatches, page, count);
        }

        public void SetRestrictionIdentifiers(int discountCouponId, IList<string> restrictionIdentifiers)
        {
            var savedValues = _restrictionValueService.Get(x => x.DiscountCouponId == discountCouponId).ToList();
            if (restrictionIdentifiers != null)
            {
                //insert the new ones
                var toInsert = new List<RestrictionValue>();
                foreach (var ri in restrictionIdentifiers)
                {
                    if (savedValues.Any(
                        x => x.RestrictionIdentifier.Equals(ri, StringComparison.InvariantCultureIgnoreCase)))
                        continue; //already saved, nothing to be done
                    toInsert.Add(new RestrictionValue() {
                        DiscountCouponId = discountCouponId,
                        RestrictionIdentifier = ri
                    });
                }
                //save the new
                _restrictionValueService.Insert(toInsert.ToArray());
            }

            if (restrictionIdentifiers == null || !restrictionIdentifiers.Any())
            {
                //delete all
                _restrictionValueService.Delete(x => x.DiscountCouponId == discountCouponId);
            }
            else
            {
                //delete the other ones
                var toDelete = savedValues
                    .Where(x => !restrictionIdentifiers.Contains(x.RestrictionIdentifier,
                        StringComparer.InvariantCultureIgnoreCase))
                    .ToList();
                foreach (var rv in toDelete)
                    _restrictionValueService.Delete(rv);
            }

        }

        private IEntitySet<DiscountCoupon> GetByWhere(Expression<Func<DiscountCoupon, bool>> where)
        {
            return Repository.Where(where)
                .Join<RestrictionValue>("Id", "DiscountCouponId", joinType: JoinType.LeftOuter)
                .Relate(RelationTypes.OneToMany<DiscountCoupon, RestrictionValue>());
        }

        public override void Insert(DiscountCoupon entity, Transaction transaction = null)
        {
            //always save in lower case
            entity.CouponCode = entity.CouponCode?.ToLower();
            base.Insert(entity, transaction);
        }

        public override void Update(DiscountCoupon entity, Transaction transaction = null)
        {
            //always save in lower case
            entity.CouponCode = entity.CouponCode?.ToLower();
            base.Update(entity, transaction);
        }
    }
}