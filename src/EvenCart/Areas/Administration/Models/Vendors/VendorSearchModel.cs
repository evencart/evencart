using EvenCart.Data.Entity.Users;
using EvenCart.Infrastructure.Mvc.Models;

namespace EvenCart.Areas.Administration.Models.Vendors
{
    public class VendorSearchModel : AdminSearchModel
    {
        public VendorStatus? VendorStatus { get; set; }

        public int? UserId { get; set; }
    }
}