using System;
using EvenCart.Data.Entity.Gdpr;
using EvenCart.Infrastructure.Mvc.Models;

namespace EvenCart.Areas.Administration.Models.Gdpr
{
    public class ConsentLogModel : FoundationModel
    {
        public string ConsentTitle { get; set; }

        public ActivityType ActivityType { get; set; }

        public DateTime CreatedOn { get; set; }

        public string UserInfo { get; set; }
    }
}