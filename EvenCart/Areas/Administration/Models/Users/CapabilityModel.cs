using EvenCart.Infrastructure.Mvc.Models;

namespace EvenCart.Areas.Administration.Models.Users
{
    public class CapabilityModel : FoundationEntityModel
    {
        public string Name { get; set; }

        public bool Active { get; set; }
    }
}