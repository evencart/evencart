using System.Collections.Generic;
using EvenCart.Data.Entity.Purchases;

namespace EvenCart.Areas.Administration.Models.Settings
{
    public class OrderSettingsModel : SettingsModel
    {
        public string OrderNumberTemplate { get; set; }

        public bool AllowReorder { get; set; }

        public bool AllowGuestCheckout { get; set; }

        public bool EnableWishlist { get; set; }

        public bool AllowReturns { get; set; }

        public int DefaultReturnsDays { get; set; }

        public bool AllowCancellation { get; set; }

        public IList<string> CancellationAllowedFor { get; set; }
    }
}