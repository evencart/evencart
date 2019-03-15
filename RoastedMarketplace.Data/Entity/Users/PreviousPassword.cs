using System;
using RoastedMarketplace.Core.Data;
using RoastedMarketplace.Data.Enum;

namespace RoastedMarketplace.Data.Entity.Users
{
    public class PreviousPassword : FoundationEntity
    {
        public int UserId { get; set; }

        public string Password { get; set; }

        public string PasswordSalt { get; set; }

        public PasswordFormat PasswordFormat { get; set; }

        public DateTime DateCreated { get; set; }
    }
}