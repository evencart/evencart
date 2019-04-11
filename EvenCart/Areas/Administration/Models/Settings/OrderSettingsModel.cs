namespace EvenCart.Areas.Administration.Models.Settings
{
    public class OrderSettingsModel : SettingsModel
    {
        public string OrderNumberTemplate { get; set; }

        public bool AllowReorder { get; set; }

        public bool AllowGuestCheckout { get; set; }

        public bool EnableWishlist { get; set; }
    }
}