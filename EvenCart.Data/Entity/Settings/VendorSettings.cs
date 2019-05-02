using EvenCart.Core.Config;

namespace EvenCart.Data.Entity.Settings
{
    public class VendorSettings : ISettingGroup
    {
        public bool EnableVendorSignup { get; set; }
    }
}