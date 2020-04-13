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
using EvenCart.Core.Data;
using EvenCart.Data.Entity.EntityProperties;
using EvenCart.Data.Enum;
using EvenCart.Data.Interfaces;

namespace EvenCart.Data.Entity.Users
{
    public class User : FoundationEntity, ISoftDeletable, IHasEntityProperties, ISearchFilterSupported
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string CompanyName { get; set; }

        public string MobileNumber { get; set; }

        public Guid Guid { get; set; }

        public string Password { get; set; }

        public string PasswordSalt { get; set; }

        public PasswordFormat PasswordFormat { get; set; }

        public bool Active { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime UpdatedOn { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public DateTime? LastLoginDate { get; set; }

        public DateTime? LastActivityDate { get; set; }

        public DateTime? FirstActivationDate { get; set; }

        public string LastActivityIpAddress { get; set; }

        public bool IsSystemAccount { get; set; }

        public string Remarks { get; set; }

        public string LastLoginIpAddress { get; set; }

        public int ReferrerId { get; set; }

        public bool Deleted { get; set; }

        public bool RequirePasswordChange { get; set; }

        public bool IsTaxExempt { get; set; }

        public bool NewslettersEnabled { get; set; }

        public string ActiveLanguageCulture { get; set; }

        public int ActiveCurrencyId { get; set; }

        public string TimeZoneId { get; set; }

        public int Points { get; set; }

        public int? ProfilePictureId { get; set; }

        public bool IsAffiliate { get; set; }

        public bool AffiliateActive { get; set; }

        public DateTime? AffiliateFirstActivationDate { get; set; }

        #region Virtual Properties
        public virtual IList<UserRole> UserRoles { get; set; }

        public virtual IList<Role> Roles { get; set; }

        public virtual IList<Capability> Capabilities { get; set; }

        public virtual IList<EntityProperty> EntityProperties { get; set; }
        #endregion
    }
}