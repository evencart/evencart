using System.ComponentModel;

namespace EvenCart.Data.Entity.Purchases
{
    public enum RefundType
    {
        [Description("To actual payment methods")]
        ToRespectivePaymentMethods = 1,
        [Description("To store credits")]
        ToStoreCredits = 3
    }
}