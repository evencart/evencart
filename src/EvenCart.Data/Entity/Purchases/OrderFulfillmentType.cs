namespace EvenCart.Data.Entity.Purchases
{
    /// <summary>
    /// The way in which order should be fulfilled
    /// </summary>
    public enum OrderFulfillmentType
    {
        /// <summary>
        /// Fulfill entire order from one warehouse
        /// </summary>
        WholeFromSingleWarehouse = 1,
        /// <summary>
        /// Fulfill order from multiple warehouses restricting a single product to one warehouse
        /// </summary>
        SplitToMultipleWarehouseByItem = 2,
        /// <summary>
        /// NSY: Fulfill order from multiple warehouses allowing single product quantities to be fulfilled across warehouses.
        /// </summary>
        SplitToMultipleWarehouseByItemQuantity = 3
    }
}