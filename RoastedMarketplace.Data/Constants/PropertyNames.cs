using System;
using System.Linq;

namespace RoastedMarketplace.Data.Constants
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