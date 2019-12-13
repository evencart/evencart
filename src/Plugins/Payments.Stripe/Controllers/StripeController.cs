using System;
using EvenCart.Infrastructure;
using EvenCart.Infrastructure.Mvc;
using EvenCart.Infrastructure.Mvc.Attributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Payments.Stripe.Helpers;
using Payments.Stripe.Models;

namespace Payments.Stripe.Controllers
{
    [Route("stripe")]
    [PluginType(PluginType = typeof(StripePlugin))]
    public class StripeController : FoundationPluginController
    {
        [HttpGet("payment-info", Name = StripeConfig.PaymentHandlerComponentRouteName)]
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

        [HttpPost("webhook", Name = StripeConfig.StripeWebhookUrl)]
        public IActionResult Webhook()
        {
            StripeHelper.ParseWebhookResponse(ApplicationEngine.CurrentHttpContext.Request);
            return Ok();
        }
    }
}