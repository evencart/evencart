using EvenCart.Infrastructure.Mvc.Models;

namespace EvenCart.Models.Authentication
{
    public class RegisterRequestModel : FoundationModel
    {
        /// <summary>
        /// The invite code if any for the request.
        /// </summary>
        public string InviteCode { get; set; }

        /// <summary>
        /// The affiliate code if any for the request
        /// </summary>
        public string AffiliateCode { get; set; }
    }
}