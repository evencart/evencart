using System;
using RoastedMarketplace.Data.Entity.Gdpr;
using RoastedMarketplace.Infrastructure.Mvc.Models;

namespace RoastedMarketplace.Areas.Administration.Models.Gdpr
{
    public class ConsentLogModel : FoundationModel
    {
        public string ConsentTitle { get; set; }

        public ActivityType ActivityType { get; set; }

        public DateTime CreatedOn { get; set; }

        public string UserInfo { get; set; }
    }
}