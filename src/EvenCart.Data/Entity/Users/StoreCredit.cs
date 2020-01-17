using System;
using EvenCart.Core.Data;

namespace EvenCart.Data.Entity.Users
{
    public class StoreCredit : FoundationEntity
    {
        public int UserId { get; set; }

        public decimal Credit { get; set; }

        public string Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime AvailableOn { get; set; }

        public DateTime? ExpiresOn { get; set; }

        public bool Locked { get; set; }
        
    }
}