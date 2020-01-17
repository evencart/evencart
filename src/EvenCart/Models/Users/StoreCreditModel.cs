using System;
using EvenCart.Infrastructure.Mvc.Models;

namespace EvenCart.Models.Users
{
    public class StoreCreditModel : FoundationModel
    {
        public decimal Credit { get; set; }

        public string Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime AvailableOn { get; set; }

        public DateTime? ExpiresOn { get; set; }
    }
}