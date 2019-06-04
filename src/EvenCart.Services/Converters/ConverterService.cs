using System;
using EvenCart.Data.Entity.Shop;

namespace EvenCart.Services.Converters
{
    public class ConverterService : IConverterService
    {
        public decimal ConvertWeight(WeightUnit source, WeightUnit destination, decimal input)
        {
            if (source == destination)
                return input;
            //convert to the lowest unit
            var inputAsGrams = input;
            switch (source)
            {
                case WeightUnit.Gram:
                    inputAsGrams = input;
                    break;
                case WeightUnit.Kilogram:
                    inputAsGrams = input * 1000;
                    break;
                case WeightUnit.Ounce:
                    inputAsGrams = input * 28.349m;
                    break;
                case WeightUnit.Pound:
                    inputAsGrams = input * 453.592m;
                    break;
                case WeightUnit.Ton:
                    inputAsGrams = input * 1000000;
                    break;
            }
            switch (destination)
            {
                case WeightUnit.Gram:
                    return inputAsGrams;
                case WeightUnit.Kilogram:
                    return inputAsGrams / 1000;
                case WeightUnit.Ounce:
                    return inputAsGrams / 28.349m;
                case WeightUnit.Pound:
                    return inputAsGrams / 453.592m;
                case WeightUnit.Ton:
                    return inputAsGrams / 1000000;
            }

            return input;
        }

        public decimal ConvertLength(LengthUnit source, LengthUnit destination, decimal input)
        {
            if (source == destination)
                return input;
            //convert all to cms
            var inputAsCms = input;
            switch (source)
            {
                case LengthUnit.Centimeter:
                    inputAsCms = input;
                    break;
                case LengthUnit.Meter:
                    inputAsCms = input * 100;
                    break;
                case LengthUnit.Feet:
                    inputAsCms = input * 12 * 2.54m;
                    break;
                case LengthUnit.Inch:
                    inputAsCms = input * 2.54m;
                    break;
                case LengthUnit.Yard:
                    inputAsCms = input * 91.44m;
                    break;
            }

            switch (destination)
            {
                case LengthUnit.Centimeter:
                    return inputAsCms;
                case LengthUnit.Meter:
                    return inputAsCms / 100;
                case LengthUnit.Feet:
                    return inputAsCms / (12 * 2.54m);
                case LengthUnit.Inch:
                    return inputAsCms / 2.54m;
                case LengthUnit.Yard:
                    return inputAsCms / 91.44m;
            }

            return input;
        }
    }
}