using System.ComponentModel;

namespace EvenCart.Data.Entity.Shop
{
    /// <summary>
    /// Represents a weight unit
    /// </summary>
    public enum WeightUnit
    {
        /// <summary>
        /// The gram unit
        /// </summary>
        [Description("gm(s)")]
        Gram = 1,
        /// <summary>
        /// The kilogram unit
        /// </summary>
        [Description("kg(s)")]
        Kilogram = 2,
        /// <summary>
        /// The ounce unit
        /// </summary>
        [Description("oz")]
        Ounce = 3,
        /// <summary>
        /// The pound unit
        /// </summary>
        [Description("lb(s)")]
        Pound = 4,
        /// <summary>
        /// The ton unit
        /// </summary>
        [Description("ton(s)")]
        Ton = 5
    }
}