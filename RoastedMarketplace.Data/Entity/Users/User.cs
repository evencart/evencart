using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RoastedMarketplace.Core.Data;
using RoastedMarketplace.Core.Infrastructure.Attributes;
using RoastedMarketplace.Data.Enum;
using RoastedMarketplace.Data.Interfaces;

namespace RoastedMarketplace.Data.Entity.Users
{
    public class User : FoundationEntity, ISoftDeletable, IHasEntityProperties<User>, ISearchFilterSupported
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Name { get; set; }

        [Required]
        [NonPatchable]
        public string Email { get; set; }

        [NonPatchable]
        public string UserName { get; set; }

        [NonPatchable]
        public Guid Guid { get; set; }

        [Required]
        [NonPatchable]
        public string Password { get; set; }

        [NonPatchable]
        public string PasswordSalt { get; set; }

        [NonPatchable]
        public PasswordFormat PasswordFormat { get; set; }

        public bool Active { get; set; }

        [NonPatchable]
        public DateTime DateCreated { get; set; }

        public DateTime DateUpdated { get; set; }

        public DateTime? LastLoginDate { get; set; }

        [NonPatchable]
        public bool IsSystemAccount { get; set; }

        public string Remarks { get; set; }

        [NonPatchable]
        public string LastLoginIpAddress { get; set; }

        [NonPatchable]
        public int ReferrerId { get; set; }

        [NonPatchable]
        public bool Deleted { get; set; }

        public virtual IList<Role> Roles { get; set; }
    }
}