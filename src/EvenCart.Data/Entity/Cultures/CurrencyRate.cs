namespace EvenCart.Data.Entity.Cultures
{
    public class CurrencyRate
    {
        public string IsoCode { get; set; }

        public decimal Rate { get; set; }

        public CurrencyRate(string isoCode, decimal rate)
        {
            IsoCode = isoCode;
            Rate = rate;
        }
    }
}