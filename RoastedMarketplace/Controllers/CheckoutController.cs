using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using RoastedMarketplace.Core;
using RoastedMarketplace.Core.Extensions;
using RoastedMarketplace.Data.Entity.Addresses;
using RoastedMarketplace.Data.Entity.Payments;
using RoastedMarketplace.Data.Entity.Purchases;
using RoastedMarketplace.Data.Entity.Settings;
using RoastedMarketplace.Data.Entity.Users;
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
using RoastedMarketplace.Services.Extensions;
using RoastedMarketplace.Services.Helpers;
using RoastedMarketplace.Services.Payments;
using RoastedMarketplace.Services.Plugins;
using RoastedMarketplace.Services.Purchases;
using RoastedMarketplace.Services.Serializers;
using RoastedMarketplace.Services.Tokens;
using RoastedMarketplace.Services.Users;

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
        private readonly OrderSettings _orderSettings;
        private readonly IRoleService _roleService;
        private readonly IUserService _userService;
        private readonly ITokenGenerator _tokenGenerator;

        public CheckoutController(IPaymentProcessor paymentProcessor, IPaymentAccountant paymentAccountant, IModelMapper modelMapper, IAddressService addressService, ICartService cartService, IDataSerializer dataSerializer, IPluginAccountant pluginAccountant, IOrderService orderService, IOrderItemService orderItemService, OrderSettings orderSettings, IRoleService roleService, IUserService userService, ITokenGenerator tokenGenerator)
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
            _orderSettings = orderSettings;
            _roleService = roleService;
            _userService = userService;
            _tokenGenerator = tokenGenerator;
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

            //validations first before inserting anything
            if (requestModel.BillingAddress.Id > 0)
            {
                //is it the valid address of current user?
                var billingAddressId = requestModel.BillingAddress.Id;
                if (_addressService.Count(x => x.UserId == currentUser.Id && x.Id == billingAddressId) == 0)
                    return R.Fail.With("error", T("Invalid billing address provided")).Result;
            }

            if (requestModel.UseDifferentShippingAddress)
            {
                if (requestModel.ShippingAddress.Id > 0)
                {
                    //is it the valid address of current user?
                    var shippingAddressId = requestModel.ShippingAddress.Id;
                    Address shippingAddress = null;
                    if ((shippingAddress = _addressService.Get(x => x.UserId == currentUser.Id && x.Id == shippingAddressId, 1, 1).FirstOrDefault()) == null)
                        return R.Fail.With("error", T("Invalid shipping address provided")).Result;

                    if (!shippingAddress.StateOrProvince?.ShippingEnabled ?? false)
                    {
                        return R.Fail.With("error", T("Shipping is not allowed in this state")).Result;
                    }

                    if (!shippingAddress.Country?.ShippingEnabled ?? false)
                    {
                        return R.Fail.With("error", T("Shipping is not allowed in this country")).Result;
                    }
                }
            }

            //shipping handler validation
            if (requestModel.ShippingMethod != null)
            {

                //validate shipping method
                var shippingHandler = PluginHelper.GetShipmentHandler(requestModel.ShippingMethod.SystemName);
                if (shippingHandler == null)
                    return R.Fail.With("error", T("Shipping method unavailable")).Result;
                cart.ShippingMethodName = requestModel.ShippingMethod.SystemName;
                cart.ShippingFee = shippingHandler.GetShippingHandlerFee(cart);
            }

            //save addresses if required
            if (requestModel.BillingAddress.Id == 0)
            {
                var address = _modelMapper.Map<Address>(requestModel.BillingAddress);
                address.UserId = currentUser.Id;
                _addressService.Insert(address);
                requestModel.BillingAddress.Id = address.Id;
            }
           
            if (requestModel.UseDifferentShippingAddress)
            {
                if (requestModel.ShippingAddress.Id == 0)
                {
                    var address = _modelMapper.Map<Address>(requestModel.ShippingAddress);
                    address.UserId = currentUser.Id;
                    _addressService.Insert(address);
                }
            }
            else
            {
                requestModel.ShippingAddress = requestModel.BillingAddress;
            }
            //save the address in the cart now
            cart.BillingAddressId = requestModel.BillingAddress.Id;
            cart.ShippingAddressId = requestModel.ShippingAddress.Id;
            _cartService.Update(cart);

            RaiseEvent(NamedEvent.OrderAddressSaved, cart);
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
                    var model = new PaymentMethodModel()
                    {
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

            RaiseEvent(NamedEvent.OrderPaymentInfoSaved, cart);
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

            var currentUser = ApplicationEngine.CurrentUser;
            var order = new Order()
            {
                PaymentMethodName = cart.PaymentMethodName,
                ShippingMethodName = cart.ShippingMethodName,
                CreatedOn = DateTime.UtcNow,
                BillingAddressId = cart.BillingAddressId,
                ShippingAddressId = cart.ShippingAddressId,
                DiscountId = cart.DiscountCouponId,
                Guid = Guid.NewGuid().ToString(),
                UserId = cart.UserId,
                PaymentStatus = PaymentStatus.Pending,
                OrderStatus = OrderStatus.New,
                DiscountCoupon =  cart.DiscountCoupon?.CouponCode,
                Discount = cart.Discount,
                PaymentMethodFee = cart.PaymentMethodFee,
                ShippingMethodFee = cart.ShippingFee,
                Tax = cart.CartItems.Sum(x => x.Tax),
                UserIpAddress = WebHelper.GetClientIpAddress(),
                CurrencyCode = ApplicationEngine.CurrentCurrencyCode,
                Subtotal = cart.FinalAmount - cart.CartItems.Sum(x => x.Tax),
            };
            order.OrderTotal = order.Subtotal + order.Tax + order.PaymentMethodFee ?? 0 +
                               order.ShippingMethodFee ?? 0;

            _orderService.Insert(order);
            //generate order number & update it
            var orderNumber = _tokenGenerator.MakeToken(new TemplateToken()
            {
                DateTime = order.CreatedOn,
                Id = order.Id,
                Template = _orderSettings.OrderNumberTemplate,
                UserId = order.UserId
            });
            order.OrderNumber = orderNumber;
            _orderService.Update(order);

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
                    return R.Redirect(transactionResult.RedirectionUrl);
            }

            //if we are here, payment has been done, so we can get the transaction data
            //create payment transaction object and save it to database
            _paymentAccountant.ProcessTransactionResult(transactionResult);

            //clear the user's cart
            _cartService.ClearCart(currentUser.Id);

            if (currentUser.IsVisitor())
            {
                //if current user is visitor, change the email to billing address and change it to registered user
                var billingAddress = _addressService.Get(order.BillingAddressId);
                currentUser.Email = billingAddress.Email;
                _userService.Update(currentUser);

                var roleId = _roleService.Get(x => x.SystemName == SystemRoleNames.Registered).First().Id;
                //assign registered role to the user
                _roleService.SetUserRoles(currentUser.Id, new[] { roleId });

                ApplicationEngine.SignIn(currentUser.Email, null, false);

            }

            return R.Success.With("orderGuid", order.Guid).Result;
        }

        [HttpGet("complete/{orderGuid}", Name = RouteNames.CheckoutComplete)]
        public IActionResult Complete(string orderGuid)
        {
            var order = _orderService.GetByGuid(orderGuid);
            if (order == null || order.UserId != ApplicationEngine.CurrentUser.Id)
                return NotFound();
            RaiseEvent(NamedEvent.OrderPlaced, order.User, order);
            return R.Success.With("orderGuid", orderGuid).With("orderNumber", order.OrderNumber).Result;
        }


        #region Helpers

        private bool CanCheckout(out Cart cart)
        {
            var currentUser = ApplicationEngine.CurrentUser;
            cart = _cartService.GetCart(currentUser.Id);
            //refresh the cart
            CartHelper.RefreshCart(cart);

            if (!_orderSettings.AllowGuestCheckout && currentUser.IsVisitor())
                return false;

            //do we have any items remaining
            return cart.CartItems.Any();
        }
        #endregion

    }
}