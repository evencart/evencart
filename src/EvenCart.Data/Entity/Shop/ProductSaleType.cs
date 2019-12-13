using System.ComponentModel;

namespace EvenCart.Data.Entity.Shop
{
    public enum ProductSaleType
    {
        [Description("One-time")]
        OneTime,
        [Description("Subscription")]
        Subscription,
        [Description("Both")]
        Both
    }
}