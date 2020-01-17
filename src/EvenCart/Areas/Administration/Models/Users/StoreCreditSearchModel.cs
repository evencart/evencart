using EvenCart.Infrastructure.Mvc.Models;

namespace EvenCart.Areas.Administration.Models.Users
{
    public class StoreCreditSearchModel : AdminSearchModel
    {
        public int UserId { get; set; }
    }
}