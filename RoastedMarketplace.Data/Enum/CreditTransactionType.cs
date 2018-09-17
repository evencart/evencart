using System;

namespace RoastedMarketplace.Data.Enum
{
    [Flags]
    public enum CreditTransactionType
    {
        /// <summary>
        /// Credits are issued to user
        /// </summary>
        Issued = 1,

        /// <summary>
        /// Some issued credits have been taken back
        /// </summary>
        IssuedRevert = 2,

        /// <summary>
        /// Credits are spent by user
        /// </summary>
        Spent = 4,

        /// <summary>
        /// Credits are refunded to user's account
        /// </summary>
        Refund = 8
    }
}