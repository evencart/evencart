﻿#region License
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
using EvenCart.Services.Orders;
using EvenCart.Services.Reviews;
using Genesis.Extensions;
using Genesis.Helpers;
using Genesis.Modules.Addresses;
using Genesis.Modules.Emails;
using Genesis.Modules.Meta;
using Genesis.Services;

namespace Genesis.Modules.Users
{
    public class EvenCartUserService : UserService
    {
        private readonly IAddressService _addressService;
        private readonly IOrderService _orderService;
        private readonly IReviewService _reviewService;
        private readonly IPreviousPasswordService _previousPasswordService;
        private readonly IEmailService _emailService;
        public EvenCartUserService(IAddressService addressService, IOrderService orderService, IReviewService reviewService, IPreviousPasswordService previousPasswordService, IEmailService emailService) : base(previousPasswordService)
        {
            _addressService = addressService;
            _orderService = orderService;
            _reviewService = reviewService;
            _previousPasswordService = previousPasswordService;
            _emailService = emailService;
        }

        public IList<User> GetUsers(string searchText, int[] restrictToRoles, Expression<Func<User, object>> orderBy, SortOrder sortOrder, int page, int count, out int totalMatches, bool negateRoleRestriction = false, Expression<Func<User, bool>> where = null)
        {
            var query = Repository
                .Where(x => !x.Deleted)
                .Join<UserRole>("Id", "UserId", joinType: JoinType.LeftOuter)
                .Join<Role>("RoleId", "Id", joinType: JoinType.LeftOuter)
                .Relate(RelationTypes.OneToMany<User, UserRole>())
                .Relate(RelationTypes.OneToMany<User, Role>());

            if (!searchText.IsNullEmptyOrWhiteSpace())
            {
                searchText = searchText.Trim();
                query = query.Where(x => x.FirstName.Contains(searchText) ||
                                         x.LastName.Contains(searchText) ||
                                         x.LastLoginIpAddress.Contains(searchText) ||
                                         x.Email.Contains(searchText));
            }

            if (restrictToRoles != null && restrictToRoles.Any())
            {
                var roleIds = restrictToRoles.ToList();
                Expression<Func<Role, bool>> roleWhere;
                if (negateRoleRestriction)
                    roleWhere = role => !roleIds.Contains(role.Id);
                else
                {
                    roleWhere = role => roleIds.Contains(role.Id);
                }
                query = query.Where(roleWhere);
            }

            if (orderBy == null)
                orderBy = x => x.Name;
            if (where != null)
                query = query.Where(where);
            query = query.OrderBy(orderBy, sortOrder == SortOrder.Ascending ? RowOrder.Ascending : RowOrder.Descending);
            query.OrderBy(x => x.Id);
            return query.SelectNestedWithTotalMatches(out totalMatches, page, count).ToList();
        }

        public User GetByUserInfo(string email, string guid = null)
        {
            var userObject = GetByWhere(x => x.Email == email);
            return guid != null && userObject?.Guid.ToString() != guid ? null : userObject;
        }

        public void AnonymizeUser(int userId)
        {
            var user = Get(userId);
            //addresses
            var addresses = _addressService.Get(x => x.EntityId == userId && x.EntityName == nameof(User)).ToList();
            var orders = _orderService.Get(x => x.UserId == userId).ToList();
            var reviews = _reviewService.Get(x => x.UserId == userId).ToList();
            var email = user.Email;
            Transaction.Initiate(transaction =>
            {
                //set deleted to true
                user.Deleted = true;
                //randomize email
                user.Email = HtmlUtility.GetRandomEmail();
                user.FirstName = string.Empty;
                user.LastName = string.Empty;
                user.Name = string.Empty;
                user.Active = false;
                user.CompanyName = string.Empty;
                user.LastLoginIpAddress = string.Empty;
                user.MobileNumber = string.Empty;
                user.Remarks = string.Empty;
                user.ReferrerId = 0;
                user.NewslettersEnabled = false;
                Update(user, transaction);

                //delete sent emails
                _emailService.Delete(
                    x => x.TosSerialized.Contains(email) || x.CcsSerialized.Contains(email) ||
                         x.BccsSerialized.Contains(email), transaction);

                //delete previous passwords
                _previousPasswordService.Delete(x => x.UserId == userId, transaction);

                //delete addresses
                foreach (var address in addresses)
                {
                    _addressService.Delete(address, transaction);
                }

                //orders
                foreach (var order in orders)
                {
                    order.BillingAddressSerialized = null;
                    order.ShippingAddressSerialized = null;
                    order.UserIpAddress = string.Empty;
                    order.UserGstNumber = string.Empty;
                    _orderService.Update(order, transaction);
                }

                //review
                foreach (var review in reviews)
                {
                    _reviewService.Delete(review, transaction);
                }
            });



        }

        public override User Get(int id)
        {
            return GetByWhere(x => x.Id == id);
        }

        private User GetByWhere(Expression<Func<User, bool>> where)
        {
            var query = Repository.Where(where)
                .Join<UserRole>("Id", "UserId", joinType: JoinType.LeftOuter)
                .Join<Role>("RoleId", "Id", joinType: JoinType.LeftOuter)
                .Join<RoleCapability>("Id", "RoleId", joinType: JoinType.LeftOuter)
                .Join<Capability>("CapabilityId", "Id", joinType: JoinType.LeftOuter)
                .Join<UserCapability>("Id", "UserId", SourceColumn.Parent, JoinType.LeftOuter)
                .Join<Capability>("CapabilityId", "Id", joinType: JoinType.LeftOuter)
                .Relate(RelationTypes.OneToMany<User, Role>())
                .Relate<Capability>((user, capability) =>
                {
                    user.Capabilities = user.Capabilities ?? new List<Capability>();
                    if (user.Capabilities.All(x => x.Id != capability.Id))
                        user.Capabilities.Add(capability);
                });

            var userObject = query.SelectNested().FirstOrDefault();
            return userObject;
        }
    }
}