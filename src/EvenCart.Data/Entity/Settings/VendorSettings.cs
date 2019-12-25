using EvenCart.Core.Config;

namespace EvenCart.Data.Entity.Settings
{
    public class VendorSettings : ISettingGroup
    {
        public bool EnableVendors { get; set; }

        public bool EnableVendorSignup { get; set; }
    }
}