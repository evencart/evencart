using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Payments.PaypalWithRedirect.Helpers;
using Payments.PaypalWithRedirect.Models;
using RoastedMarketplace.Infrastructure.Mvc;
using RoastedMarketplace.Infrastructure.Routing;
using RoastedMarketplace.Services.Payments;
using RoastedMarketplace.Services.Purchases;

namespace Payments.PaypalWithRedirect.Controllers
{
    [Route("paypal-with-redirect")]
    public class PaypalWithRedirectController : FoundationController
    {
        private readonly IPaymentAccountant _paymentAccountant;
        private readonly IOrderService _orderService;
        private readonly PaypalWithRedirectSettings _paypalWithRedirectSettings;
        public PaypalWithRedirectController(IPaymentAccountant paymentAccountant, IOrderService orderService, PaypalWithRedirectSettings paypalWithRedirectSettings)
        {
            _paymentAccountant = paymentAccountant;
            _orderService = orderService;
            _paypalWithRedirectSettings = paypalWithRedirectSettings;
        }

        [HttpGet("payment-info", Name = PaypalConfig.PaymentHandlerComponentRouteName)]
        public IActionResult PaymentInfoDisplayPage()
        {
            var model = new PaymentInfoModel();
            var baseYear = DateTime.UtcNow.Year;
            for (var i = 1; i < 13; i++)
            {
                var value = i.ToString();
                model.AvailableMonths.Add(new SelectListItem(value, value));
            }
            //50 years from now
            for (var i = 0; i < 51; i++)
            {
                var value = (baseYear + i).ToString();
                model.AvailableYears.Add(new SelectListItem(value, value));
            }
            model.Month = DateTime.UtcNow.Month;
            model.Year = DateTime.UtcNow.Year;
            return R.With("paymentInfo", model).Result;
        }

        [HttpGet("return/{orderGuid}", Name = PaypalConfig.PaypalWithRedirectReturnUrlRouteName)]
        public IActionResult Return(string orderGuid, [FromQuery] PaymentReturnModel paymentReturnModel)
        {
            var order = _orderService.GetByGuid(orderGuid);
            if (order == null)
                return NotFound();
            var transactionResult = PaypalHelper.ProcessExecution(order, paymentReturnModel, _paypalWithRedirectSettings);
            if (transactionResult.Success)
            {
                _paymentAccountant.ProcessTransactionResult(transactionResult, true);
                return RedirectToRoute(RouteNames.CheckoutComplete, new {orderGuid = order.Guid});
            }
            return R.Fail.Result;
        }

        [HttpGet("cancel", Name = PaypalConfig.PaypalWithRedirectCancelUrlRouteName)]
        public IActionResult Cancel()
        {
            return R.Success.Result;
        }

        [HttpGet("ipn", Name = PaypalConfig.PaypalWithRedirectIpnHandlerRouteName)]
        public IActionResult Ipn()
        {
            return R.Success.Result;
        }

    }
}