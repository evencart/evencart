using EvenCart.Core.Data;
using EvenCart.Data.Entity.Users;

namespace EvenCart.Data.Entity.Social
{
    public class ConnectedAccount : FoundationEntity
    {
        public int UserId { get; set; }

        public string ProviderName { get; set; }

        public string ProviderUserId { get; set; }

        public string AccessToken { get; set; }

        #region Virtual Properties
        public virtual User User { get; set; }
        #endregion
    }
}