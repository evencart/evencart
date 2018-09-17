using RoastedMarketplace.Core.Config;
using RoastedMarketplace.Data.Enum;

namespace RoastedMarketplace.Data.Entity.Settings
{
    public class PaymentSettings : ISettingGroup
    {
        /// <summary>
        /// The credit exchange rate i.e. the number of credits issued to user per unit of currency
        /// </summary>
        public decimal CreditExchangeRate { get; set; }

        /// <summary>
        /// The  limit of using promotional credits in a single transaction e.g. 10% or 15 credits
        /// </summary>
        public int PromotionalCreditUsageLimitPerTransaction { get; set; }

        /// <summary>
        /// Is the promotional credit usage limit a percentage or a number
        /// </summary>
        public bool IsPromotionalCreditUsageLimitPercentage { get; set; }

        /// <summary>
        /// Specifies how payment method is selected
        /// </summary>
        public PaymentMethodSelectionType PaymentMethodSelectionType { get; set; }

        public decimal MicroPaymentsFixedPaymentProcessingFee { get; set; }

        public decimal MacroPaymentsFixedPaymentProcessingFee { get; set; }

        public decimal MicroPaymentsPaymentProcessingPercentage { get; set; }

        public decimal MacroPaymentsPaymentProcessingPercentage { get; set; }

    }
}