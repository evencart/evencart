using EvenCart.Core.Data;

namespace EvenCart.Data.Entity.Addresses
{
    public class StateOrProvince : FoundationEntity
    {
        public int CountryId { get; set; }

        public string Name { get; set; }

        public bool Published { get; set; }

        public bool ShippingEnabled { get; set; }

        public int DisplayOrder { get; set; }

        #region Virtual Properties
        public virtual Country Country { get; set; }
        #endregion
    }
}