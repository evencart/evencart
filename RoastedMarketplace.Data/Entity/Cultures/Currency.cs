using RoastedMarketplace.Core.Data;

namespace RoastedMarketplace.Data.Entity.Cultures
{
    public class Currency : FoundationEntity
    {
        public string Name { get; set; }

        public string IsoCode { get; set; }

        public decimal ExchangeRate { get; set; }

        public string CultureCode { get; set; }

        public string CustomFormat { get; set; }

        public string Flag { get; set; }

        public bool Published { get; set; }

        public Rounding RoundingType { get; set; }

        public int NumberOfDecimalPlaces { get; set; }
    }
}