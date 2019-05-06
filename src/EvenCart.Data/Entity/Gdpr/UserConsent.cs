using EvenCart.Core.Data;
using EvenCart.Data.Entity.Users;

namespace EvenCart.Data.Entity.Gdpr
{
    public class UserConsent : FoundationEntity
    {
        public int UserId { get; set; }

        public int ConsentId { get; set; }

        public ConsentStatus ConsentStatus { get; set; }

        #region Virtual Properties
        public virtual User User { get; set; }

        public virtual Consent Consent { get; set; }
        #endregion
    }
}