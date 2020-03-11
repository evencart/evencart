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

namespace EvenCart.Data.Extensions
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
            return decimal.Ceiling(num / 100.0m) * 100;
        }

        public static decimal FloorHundred(this decimal num)
        {
            return decimal.Floor(num / 100.0m) * 100;
        }

        public static decimal RoundHundred(this decimal num)
        {
            return decimal.Round(num / 100.0m) * 100;
        }
    }
}