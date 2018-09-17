namespace RoastedMarketplace.Data.Enum
{
    public enum PaymentMethodSelectionType
    {
        /// <summary>
        /// Specifies that user should select the payment method while making payment
        /// </summary>
        UserSelected = 1,

        /// <summary>
        /// Specifies that a particular payment method should be used for all transactions
        /// </summary>
        SingleAllAmounts = 2,

        /// <summary>
        /// Specifies that different payment methods should be used for different amount ranges
        /// </summary>
        SinglePerAmountRange = 3
    }
}