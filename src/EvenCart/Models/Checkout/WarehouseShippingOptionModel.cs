using EvenCart.Infrastructure.Mvc.Models;
using EvenCart.Models.Addresses;
using System.Collections.Generic;

namespace EvenCart.Models.Checkout
{
    /// <summary>
    /// Represents shipping options grouped by warehouse
    /// </summary>
    public class WarehouseShippingOptionModel : FoundationModel
    {
        /// <summary>
        /// The id of the warehouse
        /// </summary>
        public int WarehouseId { get; set; }

        /// <summary>
        /// The <see cref="AddressInfoModel">address</see> of the warehouse
        /// </summary>
        public AddressInfoModel WarehouseAddress { get; set; }

        /// <summary>
        /// The list of available shipping options for this warehouse
        /// </summary>
        public IList<ShippingOptionModel> ShippingOptions { get; set; }
    }
}