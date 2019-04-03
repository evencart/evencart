using System.Collections.Generic;
using RoastedMarketplace.Core.Data;

namespace RoastedMarketplace.Data.Entity.Gdpr
{
    public class ConsentGroup : FoundationEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int DisplayOrder { get; set; }

        #region Virtual Properties
        public virtual IList<Consent> Consents { get; set; }
        #endregion
    }
}