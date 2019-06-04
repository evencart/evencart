using System.Collections.Generic;
using EvenCart.Core.Config;

namespace Shipping.UPS
{
    public class UPSSettings : ISettingGroup
    {
        public bool DebugMode { get; set; }

        public string ShipperNumber { get; set; }

        public string LicenseNumber { get; set; }

        public string UserId { get; set; }

        public string Password { get; set; }

        public decimal AdditionalFee { get; set; }

        public PickupType PickupType { get; set; }

        public PackagingType DefaultPackagingType { get; set; }

        public CustomerClassificationType CustomerClassificationType { get; set; }

        public IList<string> ActiveServices { get; set; }
    }
}