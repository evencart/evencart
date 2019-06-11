using EvenCart.Infrastructure.Mvc.Models;

namespace EvenCart.Areas.Administration.Models.Warehouse
{
    /// <summary>
    /// Represents a minimal warehouse object
    /// </summary>
    public class WarehouseMiniModel : FoundationEntityModel
    {
        /// <summary>
        /// The name of the warehouse.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The display order of the warehouse
        /// </summary>
        public int DisplayOrder { get; set; }
    }
}