using System;
using Microsoft.AspNetCore.Mvc;
using RoastedMarketplace.Data.Entity.Purchases;
using RoastedMarketplace.Infrastructure;
using RoastedMarketplace.Infrastructure.Mvc;
using RoastedMarketplace.Infrastructure.Routing;
using RoastedMarketplace.Services.Extensions;
using RoastedMarketplace.Services.Logger;
using RoastedMarketplace.Services.Payments;

namespace RoastedMarketplace.Controllers
{
    public class CheckoutController : FoundationController
    {
        private readonly IPaymentProcessor _paymentProcessor;
        private readonly IPaymentAccountant _paymentAccountant;
        private readonly ILogger _logger;

        public CheckoutController(IPaymentProcessor paymentProcessor, IPaymentAccountant paymentAccountant, ILogger logger)
        {
            _paymentProcessor = paymentProcessor;
            _paymentAccountant = paymentAccountant;
            _logger = logger;
        }

        [HttpGet("init", Name = RouteNames.CheckoutInit)]
        public IActionResult Initiate()
        {
            var order = new Order()
            {
                PaymentMethodName = "DefaultPaymentModule"
            };
            var transactionResult = _paymentProcessor.ProcessPayment(order);
            if (transactionResult.Success)
            {
                if (transactionResult.RequiresRedirection)
                    return Redirect(transactionResult.RedirectionUrl);
            }
            

            //if we are here, payment has been done, so we can get the transaction data
            //create payment transaction object and save it to database
            _paymentAccountant.ProcessTransactionResult(transactionResult);
            return null;
        }

        [HttpGet("complete/{orderId:int}", Name = RouteNames.CheckoutComplete)]
        public IActionResult Complete(int orderId)
        {
            return null;
        }
        
    }
}