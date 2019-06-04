using System.ComponentModel;

namespace EvenCart.Data.Entity.Shop
{
    /// <summary>
    /// Represents the length unit
    /// </summary>
    public enum LengthUnit
    {
        /// <summary>
        /// The centimeter unit
        /// </summary>
        [Description("cm(s)")]
        Centimeter = 1,
        /// <summary>
        /// The meter unit
        /// </summary>
        [Description("meter(s)")]
        Meter = 2,
        /// <summary>
        /// The feet unit
        /// </summary>
        [Description("feet")]
        Feet = 3,
        /// <summary>
        /// The inch unit
        /// </summary>
        [Description("inch(es)")]
        Inch = 4,
        /// <summary>
        /// The yard unit
        /// </summary>
        [Description("yard(s)")]
        Yard = 5
    }
}