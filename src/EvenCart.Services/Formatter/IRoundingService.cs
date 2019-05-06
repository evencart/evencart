using EvenCart.Data.Entity.Cultures;

namespace EvenCart.Services.Formatter
{
    public interface IRoundingService
    {
        decimal Round(decimal input, int decimalPlaces = 2, Rounding roundingType = Rounding.Default);
    }
}