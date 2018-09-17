using RoastedMarketplace.Core.Data;

namespace RoastedMarketplace.Data.Entity.Addresses
{
    public class StateOrProvince : FoundationEntity
    {
        public int CountryId { get; set; }

        public string Name { get; set; }

        #region Virtual Properties
        public virtual Country Country { get; set; }
        #endregion
    }
}