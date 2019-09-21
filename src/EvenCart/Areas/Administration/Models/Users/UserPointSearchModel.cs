using EvenCart.Infrastructure.Mvc.Models;

namespace EvenCart.Areas.Administration.Models.Users
{
    public class UserPointSearchModel : AdminSearchModel
    {
        public int UserId { get; set; }
    }
}