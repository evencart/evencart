using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using RoastedMarketplace.Core.Extensions;
using RoastedMarketplace.Data.Entity.Addresses;
using RoastedMarketplace.Data.Entity.Payments;
using RoastedMarketplace.Data.Entity.Purchases;
using RoastedMarketplace.Infrastructure;
using RoastedMarketplace.Infrastructure.Helpers;
using RoastedMarketplace.Infrastructure.Mvc;
using RoastedMarketplace.Infrastructure.Mvc.Attributes;
using RoastedMarketplace.Infrastructure.Mvc.ModelFactories;
using RoastedMarketplace.Infrastructure.Plugins;
using RoastedMarketplace.Infrastructure.Routing;
using RoastedMarketplace.Models.Addresses;
using RoastedMarketplace.Models.Checkout;
using RoastedMarketplace.Services.Addresses;
using RoastedMarketplace.Services.Helpers;
using RoastedMarketplace.Services.Payments;
using RoastedMarketplace.Services.Plugins;
using RoastedMarketplace.Services.Purchases;
using RoastedMarketplace.Services.Serializers;

namespace RoastedMarketplace.Controllers
{
    [Route("checkout")]
    public class CheckoutController : FoundationController
    {
        private readonly IPaymentProcessor _paymentProcessor;
        private readonly IPaymentAccountant _paymentAccountant;
        private readonly IModelMapper _modelMapper;
        private readonly IAddressService _addressService;
        private readonly ICartService _cartService;
        private readonly IDataSerializer _dataSerializer;
        private readonly IPluginAccountant _pluginAccountant;
        private readonly IOrderService _orderService;
        private readonly IOrderItemService _orderItemService;
        public CheckoutController(IPaymentProcessor paymentProcessor, IPaymentAccountant paymentAccountant, IModelMapper modelMapper, IAddressService addressService, ICartService cartService, IDataSerializer dataSerializer, IPluginAccountant pluginAccountant, IOrderService orderService, IOrderItemService orderItemService)
        {
            _paymentProcessor = paymentProcessor;
            _paymentAccountant = paymentAccountant;
            _modelMapper = modelMapper;
            _addressService = addressService;
            _cartService = cartService;
            _dataSerializer = dataSerializer;
            _pluginAccountant = pluginAccountant;
            _orderService = orderService;
            _orderItemService = orderItemService;
        }

        [HttpGet("billing-shipping", Name = RouteNames.CheckoutAddress)]
        public IActionResult AddressInfo()
        {
            if (!CanCheckout(out Cart cart))
                return RedirectToRoute(RouteNames.Home);
            var currentUser = ApplicationEngine.CurrentUser;
            //get addresses for current user
            var addresses = _addressService.Get(x => x.UserId == currentUser.Id);
            var addressModels = addresses.Select(x =>
            {
                var model = _modelMapper.Map<AddressInfoModel>(x);
                model.CountryName = x.Country.Name;
                return model;
            }).ToList();
            //is shipping required?
            var shippingRequired = CartHelper.IsShippingRequired(cart);
            //find available shipping methods
            var shippingHandlers = _pluginAccountant.GetActivePlugins(typeof(IShipmentHandlerPlugin));
            var shippingModels = shippingHandlers.Select(x =>
                {
                    var model = new ShippingMethodModel()
                    {
                        SystemName = x.SystemName,
                        Description = x.Description,
                        FriendlyName = x.Name,
                        Fee = x.LoadPluginInstance<IShipmentHandlerPlugin>().GetShippingHandlerFee(cart)
                    };
                    return model;
                })
                .ToList();
            return R.Success.With("addresses", addressModels)
                .With("shippingMethods", shippingModels)
                .With("shippingRequired", shippingRequired)
                .WithAvailableAddressTypes()
                .WithAvailableCountries()
                .Result;
        }

        [DualPost("billing-shipping", Name = RouteNames.CheckoutAddress, OnlyApi = true)]
        [ValidateModelState(ModelType = typeof(BillingShippingModel))]
        public IActionResult AddressInfoSave(BillingShippingModel requestModel)
        {
            if (!CanCheckout(out Cart cart))
                return R.Fail.Result;

            var currentUser = ApplicationEngine.CurrentUser;
            //save addresses if required
            if (requestModel.BillingAddress.Id == 0)
            {
                var address = _modelMapper.Map<Address>(requestModel.BillingAddress);
                address.UserId = currentUser.Id;
                _addressService.Insert(address);
                requestModel.BillingAddress.Id = address.Id;
            }
            else
            {
                //is it the valid address of current user?
                var billingAddressId = requestModel.BillingAddress.Id;
                if (_addressService.Count(x => x.UserId == currentUser.Id && x.Id == billingAddressId) == 0)
                    return R.Fail.With("message", T("Invalid billing address provided")).Result;
            }
            if (requestModel.UseDifferentShippingAddress)
            {
                if (requestModel.ShippingAddress.Id == 0)
                {
                    var address = _modelMapper.Map<Address>(requestModel.ShippingAddress);
                    address.UserId = currentUser.Id;
                    _addressService.Insert(address);
                }
                else
                {
                    //is it the valid address of current user?
                    var shippingAddressId = requestModel.ShippingAddress.Id;
                    if (_addressService.Count(x => x.UserId == currentUser.Id && x.Id == shippingAddressId) == 0)
                        return R.Fail.With("message", T("Invalid shipping address provided")).Result;
                }
            }
            else
            {
                requestModel.ShippingAddress = requestModel.BillingAddress;
            }
            //save the address in the cart now
            cart.BillingAddressId = requestModel.BillingAddress.Id;
            cart.ShippingAddressId = requestModel.ShippingAddress.Id;
            if (requestModel.ShippingMethod != null)
            {
                
                //validate shipping method
                var shippingHandler = PluginHelper.GetShipmentHandler(requestModel.ShippingMethod.SystemName);
                if (shippingHandler == null)
                    return R.Fail.With("message", T("Shipping method unavailable")).Result;
                cart.ShippingMethodName = requestModel.ShippingMethod.SystemName;
                cart.ShippingFee = shippingHandler.GetShippingHandlerFee(cart);
            }

            _cartService.Update(cart);
            return R.Success.Result;
        }

        [HttpGet("payment", Name = RouteNames.CheckoutPayment)]
        public IActionResult PaymentInfo()
        {
            if (!CanCheckout(out Cart cart))
                return RedirectToRoute(RouteNames.Home);

            //is payment required
            if (!CartHelper.IsPaymentRequired(cart))
                return RedirectToRoute(RouteNames.CheckoutConfirm);

            //find available payment methods
            var paymentHandlers = _pluginAccountant.GetActivePlugins(typeof(IPaymentHandlerPlugin));
            var paymentModels = paymentHandlers.Select(x =>
                {
                    var pluginHandlerInstance = x.LoadPluginInstance<IPaymentHandlerPlugin>();
                    var model = new PaymentMethodModel() {
                        SystemName = x.SystemName,
                        Description = x.Description,
                        FriendlyName = x.Name,
                        Fee = pluginHandlerInstance.GetPaymentHandlerFee(cart),
                        Url = Url.RouteUrl(pluginHandlerInstance.PaymentHandlerComponentRouteName)
                    };
                    return model;
                })
                .ToList();
            return R.Success.With("paymentMethods", paymentModels).Result;
        }

        [DualPost("payment", Name = RouteNames.CheckoutPayment, OnlyApi = true)]
        [ValidateModelState(ModelType = typeof(PaymentMethodModel))]
        public IActionResult PaymentInfoSave(PaymentMethodModel requestModel)
        {
            if (!CanCheckout(out Cart cart))
                return R.Fail.Result;
            //check if payment method is valid
            var paymentHandler = PluginHelper.GetPaymentHandler(requestModel.SystemName);
            if (paymentHandler == null)
                return R.Fail.With("error", T("Payment method unavailable")).Result;
            var formAsDictionary =
                requestModel.FormCollection.Keys.ToDictionary(x => x, x => requestModel.FormCollection[x].ToString());
            if (!paymentHandler.ValidatePaymentInfo(formAsDictionary, out string error))
            {
                return R.Fail.With("error", error).Result;
            }

            //if we are here, the payment method can be saved
            cart.PaymentMethodName = requestModel.SystemName;
            cart.PaymentMethodData = _dataSerializer.Serialize(formAsDictionary);
            _cartService.Update(cart);
            return R.Success.Result;
        }

        [HttpGet("confirm", Name = RouteNames.CheckoutConfirm)]
        public IActionResult Confirm()
        {
            if (!CanCheckout(out Cart cart))
                return RedirectToRoute(RouteNames.Home);
            return R.Success.Result;
        }

        [DualPost("confirm", Name = RouteNames.CheckoutConfirm, OnlyApi = true)]
        public IActionResult ConfirmSave()
        {
            if (!CanCheckout(out Cart cart))
                return R.Fail.Result;

            var order = new Order() {
                PaymentMethodName = cart.PaymentMethodName,
                ShippingMethodName = cart.ShippingMethodName,
                CreatedOn = DateTime.UtcNow,
                BillingAddressId = cart.BillingAddressId,
                ShippingAddressId = cart.ShippingAddressId,
                DiscountId = cart.DiscountCouponId,
                Guid = Guid.NewGuid().ToString(),
                UserId = cart.UserId,
                PaymentStatus = PaymentStatus.Pending,
                OrderStatus = OrderStatus.New
            };
           
            _orderService.Insert(order);

            var orderItems = new List<OrderItem>();
            foreach (var cartItem in cart.CartItems)
            {
                var orderItem = new OrderItem()
                {
                    AttributeJson = cartItem.AttributeJson,
                    OrderId = order.Id,
                    Price = cartItem.Price,
                    ProductId = cartItem.ProductId,
                    Quantity = cartItem.Quantity,
                    Tax = cartItem.Tax,
                    TaxPercent = cartItem.TaxPercent
                };
                orderItems.Add(orderItem);
            }
            //save the order items
            _orderItemService.Insert(orderItems.ToArray());

            //process payment
            var paymentMethodData = cart.PaymentMethodData.IsNullEmptyOrWhitespace()
                ? null
                : _dataSerializer.DeserializeAs<Dictionary<string, object>>(cart.PaymentMethodData);

            var transactionResult = _paymentProcessor.ProcessPayment(order, paymentMethodData);
            if (transactionResult.Success)
            {
                if (transactionResult.RequiresRedirection)
                    return Redirect(transactionResult.RedirectionUrl);
            }

            //if we are here, payment has been done, so we can get the transaction data
            //create payment transaction object and save it to database
            _paymentAccountant.ProcessTransactionResult(transactionResult);

            //clear the user's cart
            _cartService.ClearCart(ApplicationEngine.CurrentUser.Id);

            return RedirectToRoute(RouteNames.CheckoutComplete, new {orderId = order.Id});
        }

        [HttpGet("complete/{orderId:int}", Name = RouteNames.CheckoutComplete)]
        public IActionResult Complete(int orderId)
        {
            return R.Success.Result;
        }


        #region Helpers

        private bool CanCheckout(out Cart cart)
        {
            var currentUser = ApplicationEngine.CurrentUser;
            cart = _cartService.GetCart(currentUser.Id);
            //refresh the cart
            CartHelper.RefreshCart(cart);

            //do we have any items remaining
            return cart.CartItems.Any();
        }
        #endregion

    }
}