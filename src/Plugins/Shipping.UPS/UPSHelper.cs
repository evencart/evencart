using System;

namespace Shipping.UPS
{
    public static class UPSHelper
    {
        public static string GetPickupTypeCode(PickupType pickupType)
        {
            switch (pickupType)
            {
                case PickupType.DailyPickup:
                    return "01";
                case PickupType.CustomerCounter:
                    return "03";
                case PickupType.OneTimePickup:
                    return "06";
                case PickupType.LetterCenter:
                    return "19";
                case PickupType.AirServiceCenter:
                    return "20";
                default:
                    throw new Exception("Unknown UPS pickup type code");
            }
        }

        public static string GetPackagingTypeCode(PackagingType packagingType)
        {
            switch (packagingType)
            {
                case PackagingType.Letter:
                    return "01";
                case PackagingType.Package:
                    return "02";
                case PackagingType.Tube:
                    return "03";
                case PackagingType.Pak:
                    return "04";
                case PackagingType.ExpressBox:
                    return "21";
                case PackagingType.KgBox25:
                    return "24";
                case PackagingType.KgBox10:
                    return "25";
                case PackagingType.Pallet:
                    return "30";
                case PackagingType.SmallExpressBox:
                    return "2a";
                case PackagingType.MediumExpressBox:
                    return "2b";
                case PackagingType.LargeExpressBox:
                    return "2c";
                default:
                    throw new Exception("Unknown UPS packaging type code");
            }
        }

        public static string GetCustomerClassificationTypeCode(CustomerClassificationType customerClassificationType)
        {
            switch (customerClassificationType)
            {
                case CustomerClassificationType.Daily:
                    return "01";
                case CustomerClassificationType.Retail:
                    return "04";
                default:
                    throw new Exception("Unknown UPS customer classification type code");
            }
        }
    }
}