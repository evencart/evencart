using System;
using EvenCart.Data.Entity.Shop;

namespace Shipping.Shippo
{
    public static class Helper
    {
        public static string GetDistanceUnit(LengthUnit unit)
        {
            switch (unit)
            {
                case LengthUnit.Centimeter:
                    return "cm";
                case LengthUnit.Inch:
                    return "in";
                case LengthUnit.Feet:
                    return "ft";
                case LengthUnit.Meter:
                    return "m";
                case LengthUnit.Yard:
                    return "yd";
                default:
                    throw new Exception("Unknown Shippo Distance Unit");
            }
        }
        public static string GetMassUnit(WeightUnit unit)
        {
            switch (unit)
            {
                case WeightUnit.Gram:
                    return "g";
                case WeightUnit.Kilogram:
                    return "kg";
                case WeightUnit.Ounce:
                    return "oz";
                case WeightUnit.Pound:
                    return "lb";
                default:
                    throw new Exception("Unknown Shippo Mass Unit");
            }
        }
    }
}