using System;
using EvenCart.Core.Data;
using EvenCart.Data.Enum;

namespace EvenCart.Data.Entity.Users
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