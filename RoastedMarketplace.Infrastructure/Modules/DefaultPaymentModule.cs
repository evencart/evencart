using RoastedMarketplace.Core.Infrastructure.Routing;
using RoastedMarketplace.Data.Entity.Payments;
using RoastedMarketplace.Data.Entity.Purchases;
using RoastedMarketplace.Services.Modules;
using RoastedMarketplace.Services.Payments;

namespace RoastedMarketplace.Infrastructure.Modules
{
    public class DefaultPaymentModule : IPaymentHandlerModule
    {
        public bool IsSystemModule => false;
        public bool IsEmbeddedModule => true;
        public void Install()
        {
            
        }

        public void Uninstall()
        {
            
        }

        public RouteData GetConfigurationPageRouteData()
        {
            throw new System.NotImplementedException();
        }

        public RouteData GetDisplayPageRouteData()
        {
            throw new System.NotImplementedException();
        }

        public PaymentMethodType PaymentMethodType => PaymentMethodType.Misc;

        public string SystemName => "DefaultPaymentModule";

        public string Guid => "F3595DE9-3494-4148-AD86-420DB36E8B90";

        public string PaymentHandlerComponentName { get; }

        public PaymentOperation[] SupportedOperations => new []
            {PaymentOperation.Authorize, PaymentOperation.Capture, PaymentOperation.Refund, PaymentOperation.Void,};

        public TransactionResult ProcessTransaction(TransactionRequest request)
        {
            var response = new TransactionResult()
            {
                Success = true
            };
            return response.Redirect("http://www.google.com");
        }

        public decimal GetPaymentHandlerFee(Cart cart)
        {
            return 0;
        }
    }
}