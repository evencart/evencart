using EvenCart.Core.Data;
using EvenCart.Data.Enum;

namespace EvenCart.Data.Entity.Shop
{
    public class ProductRelation : FoundationEntity
    {
        public int SourceProductId { get; set; }

        public int DestinationProductId { get; set; }

        public ProductRelationType RelationType { get; set; }

        public bool IsReciprocal { get; set; }

        #region Virtual Properties
        public virtual Product DestinationProduct { get; set; }
        #endregion
    }
}