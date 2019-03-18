namespace RoastedMarketplace.Data.Extensions
{
    public static class NumericExtensions
    {
        public static decimal CeilTen(this decimal num)
        {
            return decimal.Ceiling(num / 10.0m) * 10;
        }

        public static decimal FloorTen(this decimal num)
        {
            return decimal.Floor(num / 10.0m) * 10;
        }

        public static decimal RoundTen(this decimal num)
        {
            return decimal.Round(num / 10.0m) * 10;
        }

        public static decimal CeilHundred(this decimal num)
        {
            return decimal.Ceiling(num / 100.0m) * 10;
        }

        public static decimal FloorHundred(this decimal num)
        {
            return decimal.Floor(num / 10.0m) * 10;
        }

        public static decimal RoundHundred(this decimal num)
        {
            return decimal.Round(num / 10.0m) * 10;
        }
    }
}