using EvenCart.Infrastructure.Mvc.Models;

namespace EvenCart.Models.Authentication
{
    public class InviteRequestModel : FoundationModel
    {
        public string Email { get; set; }
    }
}