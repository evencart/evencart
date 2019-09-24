using System;
using EvenCart.Services.Payments;
using EvenCart.Services.Purchases;
using EvenCart.Infrastructure.Mvc;
using EvenCart.Infrastructure.Mvc.Attributes;
using EvenCart.Infrastructure.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Payments.PaypalWithRedirect.Helpers;
using Payments.PaypalWithRedirect.Models;

namespace Payments.PaypalWithRedirect.Controllers
{
    [Route("paypal-with-redirect")]
    [PluginType(PluginType = typeof(PaypalWithRedirectPlugin))]
    public class PaypalWithRedirectController : FoundationPluginController
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

        [HttpGet("cancel/{orderGuid}", Name = PaypalConfig.PaypalWithRedirectCancelUrlRouteName)]
        public IActionResult Cancel(string orderGuid, string token)
        {
            return RedirectToRoute(RouteNames.CheckoutPayment, new {orderGuid, error = true});
        }

    }
}