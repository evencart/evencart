using System;
using EvenCart.Infrastructure.Extensions;
using EvenCart.Infrastructure.Mvc.Models;

namespace EvenCart.Models.Users
{
    public class UserMiniModel : FoundationEntityModel
    {
        public string Name { get; set; }

        public string ProfilePictureUrl { get; set; }

        public int Points { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime CreatedOnLocal => CreatedOn.ToUserDateTime();
    }
}