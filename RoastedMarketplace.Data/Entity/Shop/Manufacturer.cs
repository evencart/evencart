using RoastedMarketplace.Core.Data;
using RoastedMarketplace.Data.Entity.Pages;

namespace RoastedMarketplace.Data.Entity.Shop
{
    public class Manufacturer : FoundationEntity
    {
        public string Name { get; set; }

        #region Virtual Properties
        public virtual SeoMeta SeoMeta { get; set; }
        #endregion
    }
}