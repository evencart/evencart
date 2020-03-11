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
using System.Linq;

namespace EvenCart.Data.Constants
{
    public class PropertyNames
    {
        public const string ActivationCode = "ActivationCode";

        public const string DisplayName = "DisplayName";

        public const string DefaultPictureId = "DefaultPictureId";

        public const string DefaultCoverId = "DefaultCoverId";

        public const string DefaultTimeZoneId = "DefaultTimeZoneId";

        public static bool IsSystemPropertyName(string propertyName)
        {
            return new[]
            {
                DisplayName, DefaultPictureId, DefaultCoverId
            }.Contains(propertyName);
        }

        public static bool IsMediaPropertyName(string propertyName)
        {
            return new[]
           {
                DefaultPictureId, DefaultCoverId
            }.Contains(propertyName);
        }

        public static string ParseToValidSystemPropertyName(string propertyName)
        {
            return new[]
            {
                DisplayName, DefaultPictureId, DefaultCoverId
            }.FirstOrDefault(x => string.Compare(x, propertyName, StringComparison.InvariantCultureIgnoreCase) == 0);
        }
    }
}