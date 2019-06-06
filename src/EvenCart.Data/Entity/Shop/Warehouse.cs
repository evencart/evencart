using EvenCart.Core.Data;
using EvenCart.Data.Entity.Addresses;

namespace EvenCart.Data.Entity.Shop
{
    public class Warehouse : FoundationEntity
    {
        public int AddressId { get; set; }

        public int DisplayOrder { get; set; }

        #region Virtual Properties
        public virtual Address Address { get; set; }
        #endregion
    }
}