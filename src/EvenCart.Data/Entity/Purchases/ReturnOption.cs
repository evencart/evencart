using System.ComponentModel;

namespace EvenCart.Data.Entity.Purchases
{
    /// <summary>
    /// Represents a return option 
    /// </summary>
    public enum ReturnOption
    {
        /// <summary>
        /// The customer should be refunded
        /// </summary>
        [Description("Refund the money to the customer")]
        Refund = 10,
        /// <summary>
        /// A return order should be created for fresh items
        /// </summary>
        [Description("Create a new order to return fresh items")]
        ReturnFresh = 20,
        /// <summary>
        /// A return order should be created for repaired items
        /// </summary>
        [Description("Create a new order to return repaird items")]
        ReturnRepaired = 30,
        /// <summary>
        /// Any other option
        /// </summary>
        [Description("Other")]
        Other = 40
    }
}