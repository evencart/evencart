using EvenCart.Infrastructure.Mvc.Models;

namespace EvenCart.Areas.Administration.Models.Warehouse
{
    /// <summary>
    /// The inventory object
    /// </summary>
    public class InventoryModel : FoundationModel
    {
        /// <summary>
        /// The identifier for a single item. Can be a product id or variant id depending on if the product has variants
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The id of the warehouse
        /// </summary>
        public int WarehouseId { get; set; }

        /// <summary>
        /// The total quantity of the item
        /// </summary>
        public int TotalQuantity { get; set; }
    }
}