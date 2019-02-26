using RoastedMarketplace.Core.Data;
using RoastedMarketplace.Data.Entity.Addresses;

namespace RoastedMarketplace.Data.Entity.Taxes
{
    public class TaxRate : FoundationEntity
    {
        public int TaxId { get; set; }

        public int CountryId { get; set; }

        public int? StateOrProvinceId { get; set; }

        public string ZipOrPostalCode { get; set; }

        public decimal Rate { get; set; }

        #region Virtual Properties
        public virtual Tax Tax { get; set; }

        public virtual Country Country { get; set; }

        public virtual StateOrProvince StateOrProvince { get; set; }
        #endregion
    }
}