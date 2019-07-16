using System;
using EvenCart.Infrastructure.Extensions;
using EvenCart.Infrastructure.Mvc.Models;
using EvenCart.Models.Users;

namespace EvenCart.Models.Pages
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

        public DateTime CreatedOnLocal => CreatedOn.ToUserDateTime();

        public DateTime UpdatedOn { get; set; }

        public DateTime UpdatedOnLocal => UpdatedOn.ToUserDateTime();

        public DateTime PublishedOn { get; set; }

        public UserMiniModel User { get; set; }
    }
}