using RoastedMarketplace.Core.Config;

namespace RoastedMarketplace.Data.Entity.Settings
{
    public class OrderSettings : ISettingGroup
    {
        public string OrderNumberTemplate { get; set; }

        public bool AllowReorder { get; set; }

        public bool AllowGuestCheckout { get; set; }

        public bool EnableWishlist { get; set; }
    }
}