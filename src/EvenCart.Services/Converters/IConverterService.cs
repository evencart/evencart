using EvenCart.Data.Entity.Shop;

namespace EvenCart.Services.Converters
{
    public interface IConverterService
    {
        decimal ConvertWeight(WeightUnit source, WeightUnit destination, decimal input);

        decimal ConvertLength(LengthUnit source, LengthUnit destination, decimal input);
    }
}