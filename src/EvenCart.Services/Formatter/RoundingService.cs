#region License
// Copyright (c) Sojatia Infocrafts Private Limited.
// The following code is part of EvenCart eCommerce Software (https://evencart.co) Dual Licensed under the terms of
// 
// 1. GNU GPLv3 with additional terms (available to read at https://evencart.co/license)
// 2. EvenCart Proprietary License (available to read at https://evencart.co/license/commercial-license).
// 
// You can select one of the above two licenses according to your requirements. The usage of this code is
// subject to the terms of the license chosen by you.
#endregion

using System;
using EvenCart.Data.Entity.Cultures;

namespace EvenCart.Services.Formatter
{
    public class RoundingService : IRoundingService
    {
        public decimal Round(decimal input, int decimalPlaces = 2, Rounding roundingType = Rounding.Default)
        {
            var rounded = Math.Round(input, decimalPlaces, MidpointRounding.ToEven);
            if (decimalPlaces == 1)
                return rounded; //do nothing else
            switch (roundingType)
            {
                case Rounding.Default:
                    return rounded;
                case Rounding.RoundDot00:
                    return Math.Round(rounded); //round to nearest whole number
                case Rounding.RoundDot99:
                case Rounding.RoundDot99Or49:
                case Rounding.RoundDot50Or00:
                case Rounding.RoundDotX5:
                case Rounding.RoundDotX0:
                    var integralPart = Math.Truncate(rounded);
                    var fractionalPart = rounded - integralPart;
                    var divisor = (decimal)Math.Pow(10, decimalPlaces);
                    var lastTwoDigits = (fractionalPart * divisor) % 100;

                    if (roundingType == Rounding.RoundDot50Or00 || roundingType == Rounding.RoundDot99Or49)
                    {
                        var refPoint = roundingType == Rounding.RoundDot50Or00 ? 50 : 49;
                        var endPoint = roundingType == Rounding.RoundDot50Or00 ? 100 : 99;

                        var leftDiff = lastTwoDigits > refPoint ? lastTwoDigits - refPoint : lastTwoDigits;
                        var rightDiff = lastTwoDigits > refPoint ? endPoint - lastTwoDigits : refPoint - lastTwoDigits;

                        if (roundingType == Rounding.RoundDot99Or49 && lastTwoDigits < refPoint)
                            leftDiff++;

                        var diff = Math.Min(leftDiff, rightDiff);

                        var diffFraction = diff / divisor;
                        rounded = leftDiff == diff ? rounded - diffFraction : rounded + diffFraction;
                        return rounded;
                    }
                    else if (roundingType == Rounding.RoundDot99)
                    {
                        var leftDiff = lastTwoDigits + 1;
                        var rightDiff = 99 - lastTwoDigits;
                        var diff = Math.Min(leftDiff, rightDiff);

                        var diffFraction = diff / divisor;
                        rounded = leftDiff == diff ? rounded - diffFraction : rounded + diffFraction;
                        return rounded;
                    }
                    else if (roundingType == Rounding.RoundDotX5)
                    {
                        var lastDigit = lastTwoDigits % 10;
                        var refDigit = 5;
                        if (lastDigit == refDigit)
                            return rounded; //nothing needs to be done
                        if (lastDigit == 1 || lastDigit == 2)
                            return rounded - lastDigit / divisor;
                        if(lastDigit == 6 || lastDigit == 7)
                            return rounded - (lastDigit - refDigit) / divisor;
                        else
                        {
                            if (lastDigit < refDigit)
                                return rounded + (refDigit - lastDigit) / divisor;
                            else
                            {
                                return rounded + (refDigit * 2 - lastDigit) / divisor;
                            }
                        }
                    }
                    else if (roundingType == Rounding.RoundDotX0)
                    {
                        var lastDigit = lastTwoDigits % 10;
                        var refDigit = 0;
                        if (lastDigit == refDigit)
                            return rounded; //nothing needs to be done
                        if (lastDigit < 5)
                            return rounded - lastDigit / divisor;
                        else
                        {
                            return rounded + (10 - lastDigit) / divisor;
                        }
                    }
                    break;
                default:
                    return rounded;
            }

            return rounded;
        }
    }
}