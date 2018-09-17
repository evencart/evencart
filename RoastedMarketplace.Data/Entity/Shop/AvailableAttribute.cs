using System.Collections.Generic;
using RoastedMarketplace.Core.Data;

namespace RoastedMarketplace.Data.Entity.Shop
{
    public class AvailableAttribute : FoundationEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        #region Virtual Properties

        public virtual IList<AvailableAttributeValue> AvailableAttributeValues { get; set; }
        
        #endregion
    }
}