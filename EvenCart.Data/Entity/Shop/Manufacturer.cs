using EvenCart.Core.Data;
using EvenCart.Data.Entity.Pages;

namespace EvenCart.Data.Entity.Shop
{
    public class Manufacturer : FoundationEntity
    {
        public string Name { get; set; }

        #region Virtual Properties
        public virtual SeoMeta SeoMeta { get; set; }
        #endregion
    }
}