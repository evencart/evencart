using System;
using EvenCart.Data.Enum;
using EvenCart.Infrastructure.Mvc.Models;

namespace EvenCart.Areas.Administration.Models.Logger
{
    public class LogModel : FoundationEntityModel
    {
        public LogLevel LogLevel { get; set; }

        public string ShortMessage { get; set; }

        public string Details { get; set; }

        public string IpAddress { get; set; }

        public string Url { get; set; }

        public string ReferralUrl { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}