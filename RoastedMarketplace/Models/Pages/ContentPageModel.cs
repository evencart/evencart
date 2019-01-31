using System;
using RoastedMarketplace.Infrastructure.Mvc.Models;
using RoastedMarketplace.Models.Users;

namespace RoastedMarketplace.Models.Pages
{
    public class ContentPageModel : FoundationModel
    {
        public string Name { get; set; }

        public string Content { get; set; }

        public bool Published { get; set; }

        public bool Private { get; set; }

        public string Password { get; set; }

        public string SystemName { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime UpdatedOn { get; set; }

        public DateTime PublishedOn { get; set; }

        public UserMiniModel User { get; set; }
    }
}