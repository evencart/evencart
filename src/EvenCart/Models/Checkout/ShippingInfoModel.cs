using System.Collections.Generic;
using EvenCart.Infrastructure.Mvc.Models;

namespace EvenCart.Models.Checkout
{
    /// <summary>
    /// Represents a shipping information model
    /// </summary>
    public class ShippingInfoModel : FoundationModel
    {
        /// <summary>
        /// The <see cref="ShippingMethodModel">shippingMethod</see> to be used. Ignore if not applicable.
        /// </summary>
        public ShippingMethodModel ShippingMethod { get; set; }

        /// <summary>
        /// The list of <see cref="ShippingOptionModel">shipping option</see> to be used. Ignore if not applicable.
        /// </summary>
        public IList<ShippingOptionModel> ShippingOption { get; set; }
    }
}