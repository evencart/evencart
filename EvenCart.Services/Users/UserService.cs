using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DotEntity;
using DotEntity.Enumerations;
using EvenCart.Core.Services;
using EvenCart.Data.Entity.Users;
using EvenCart.Data.Extensions;
using EvenCart.Data.Helpers;
using EvenCart.Services.Addresses;
using EvenCart.Services.Emails;
using EvenCart.Services.Purchases;
using EvenCart.Services.Reviews;

namespace EvenCart.Services.Users
{
    public class UserService : FoundationEntityService<User>, IUserService
    {
        private readonly IAddressService _addressService;
        private readonly IOrderService _orderService;
        private readonly IReviewService _reviewService;
        private readonly IPreviousPasswordService _previousPasswordService;
        private readonly IEmailService _emailService;
        public UserService(IAddressService addressService, IOrderService orderService, IReviewService reviewService, IPreviousPasswordService previousPasswordService, IEmailService emailService)
        {
            _addressService = addressService;
            _orderService = orderService;
            _reviewService = reviewService;
            _previousPasswordService = previousPasswordService;
            _emailService = emailService;
        }

        public IList<User> GetUsers(string searchText, int[] restrictToRoles, int page, int count, out int totalMatches)
        {
            var query = Repository
                .Where(x => !x.Deleted)
                .Join<UserRole>("Id", "UserId", joinType: JoinType.LeftOuter)
                .Join<Role>("RoleId", "Id", joinType: JoinType.LeftOuter)
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
                Expression<Func<Role, bool>> roleWhere = role => roleIds.Contains(role.Id);
                query = query.Where(roleWhere);
            }
            query = query.OrderBy(x => x.Name);
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
            var addresses = _addressService.Get(x => x.UserId == userId).ToList();
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
                    order.BillingAddressId = 0;
                    order.ShippingAddressId = 0;
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