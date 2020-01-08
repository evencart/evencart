using System;
using System.Collections.Generic;
using EvenCart.Infrastructure.Mvc.Models;

namespace EvenCart.Areas.Administration.Models.Emails
{
    public class EmailMessageModel : FoundationEntityModel
    {
        public IList<string> Tos { get; set; }

        public IList<string> Ccs { get; set; }

        public IList<string> Bccs { get; set; }

        public IList<string> ReplyTos { get; set; }

        public string Subject { get; set; }

        public string EmailBody { get; set; }

        public bool IsEmailBodyHtml { get; set; }

        public DateTime SendingDate { get; set; }

        public bool IsSent { get; set; }

        public bool DeleteExisting { get; set; }
    }
}