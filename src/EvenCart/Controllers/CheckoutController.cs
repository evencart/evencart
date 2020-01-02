using System;
using System.Collections.Generic;
using System.Linq;
using EvenCart.Core;
using EvenCart.Data.Entity.Addresses;
using EvenCart.Data.Entity.Payments;
using EvenCart.Data.Entity.Purchases;
using EvenCart.Data.Entity.Settings;
using EvenCart.Data.Entity.Shop;
using EvenCart.Data.Entity.Users;
using EvenCart.Data.Enum;
using EvenCart.Data.Extensions;
using EvenCart.Services.Addresses;
using EvenCart.Services.Extensions;
using EvenCart.Services.Helpers;
using EvenCart.Services.Payments;
using EvenCart.Services.Plugins;
using EvenCart.Services.Purchases;
using EvenCart.Services.Serializers;
using EvenCart.Services.Users;
using EvenCart.Events;
using EvenCart.Infrastructure;
using EvenCart.Infrastructure.Mvc;
using EvenCart.Infrastructure.Mvc.Attributes;
using EvenCart.Infrastructure.Mvc.ModelFactories;
using EvenCart.Infrastructure.Plugins;
using EvenCart.Infrastructure.Routing;
using EvenCart.Models.Addresses;
using EvenCart.Models.Checkout;
using EvenCart.Services.Logger;
using EvenCart.Services.Products;
using Microsoft.AspNetCore.Mvc;

namespace EvenCart.Controllers
{
    /// <summary>
    /// Allows authenticated users to perform checkout activities
    /// </summary>
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
        private readonly OrderSettings _orderSettings;
        private readonly IRoleService _roleService;
        private readonly IUserService _userService;
        private readonly IProductService _productService;
        private readonly IOrderAccountant _orderAccountant;
        private readonly IDownloadService _downloadService;
        private readonly ILogger _logger;
        public CheckoutController(IPaymentProcessor paymentProcessor, IPaymentAccountant paymentAccountant, IModelMapper modelMapper, IAddressService addressService, ICartService cartService, IDataSerializer dataSerializer, IPluginAccountant pluginAccountant, IOrderService orderService, OrderSettings orderSettings, IRoleService roleService, IUserService userService, IProductService productService, IOrderAccountant orderAccountant, IDownloadService downloadService, ILogger logger)
        {
            _paymentProcessor = paymentProcessor;
            _paymentAccountant = paymentAccountant;
            _modelMapper = modelMapper;
            _addressService = addressService;
            _cartService = cartService;
            _dataSerializer = dataSerializer;
            _pluginAccountant = pluginAccountant;
            _orderService = orderService;
            _orderSettings = orderSettings;
            _roleService = roleService;
            _userService = userService;
            _productService = productService;
            _orderAccountant = orderAccountant;
            _downloadService = downloadService;
            _logger = logger;
        }

        [HttpGet("billing-shipping", Name = RouteNames.CheckoutAddress)]
        public IActionResult AddressInfo()
        {
            if (!CanCheckout(out Cart cart))
                return RedirectToRoute(RouteNames.Home);
            var currentUser = ApplicationEngine.CurrentUser;
            //get addresses for current user
            var addresses = _addressService.Get(x => x.EntityId == currentUser.Id && x.EntityName == nameof(User));
            var addressModels = addresses.Select(x =>
            {
                var model = _modelMapper.Map<AddressInfoModel>(x);
                model.CountryName = x.Country.Name;
                return model;
            }).ToList();
            var shippingRequired = CartHelper.IsShippingRequired(cart);
            return R.Success.With("addresses", addressModels)
                .With("shippingRequired", shippingRequired)
                .WithAvailableAddressTypes()
                .WithAvailableCountries()
                .Result;
        }

        /// <summary>
        /// Saves the address information for the authenticated user's cart
        /// </summary>
        /// <param name="requestModel"></param>
        /// <response code="200">A success response object</response>
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
                Address billingAddress = null;
                if ((billingAddress = _addressService.Get(x => x.EntityId == currentUser.Id && x.Id == billingAddressId, 1, 1).FirstOrDefault()) == null)
                    return R.Fail.With("error", T("Invalid billing address provided")).Result;
                if (!requestModel.UseDifferentShippingAddress)
                {
                    if (!billingAddress.StateOrProvince?.ShippingEnabled ?? false)
                    {
                        return R.Fail.With("error", T("Shipping is not allowed in this state")).Result;
                    }

                    if (!billingAddress.Country?.ShippingEnabled ?? false)
                    {
                        return R.Fail.With("error", T("Shipping is not allowed in this country")).Result;
                    }
                }
            }

            if (requestModel.UseDifferentShippingAddress)
            {
                if (requestModel.ShippingAddress.Id > 0)
                {
                    //is it the valid address of current user?
                    var shippingAddressId = requestModel.ShippingAddress.Id;
                    Address shippingAddress = null;
                    if ((shippingAddress = _addressService.Get(x => x.EntityId == currentUser.Id && x.Id == shippingAddressId, 1, 1).FirstOrDefault()) == null)
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

            //save addresses if required
            if (requestModel.BillingAddress.Id == 0)
            {
                var address = _modelMapper.Map<Address>(requestModel.BillingAddress);
                address.EntityId = currentUser.Id;
                address.EntityName = nameof(User);
                _addressService.Insert(address);
                requestModel.BillingAddress.Id = address.Id;
                cart.BillingAddress = address;
            }

            if (requestModel.UseDifferentShippingAddress)
            {
                if (requestModel.ShippingAddress.Id == 0)
                {
                    var address = _modelMapper.Map<Address>(requestModel.ShippingAddress);
                    address.EntityId = currentUser.Id;
                    address.EntityName = nameof(User);
                    _addressService.Insert(address);
                    cart.ShippingAddress = address;
                }
            }
            else
            {
                requestModel.ShippingAddress = requestModel.BillingAddress;
                cart.ShippingAddress = cart.BillingAddress;
            }

            //save the address in the cart now
            cart.BillingAddressId = requestModel.BillingAddress.Id;
            cart.ShippingAddressId = requestModel.ShippingAddress.Id;
            //clear the shipping methods
            cart.ShippingMethodName = string.Empty;
            cart.ShippingMethodDisplayName = string.Empty;
            cart.ShippingOptionsSerialized = string.Empty;
            _cartService.Update(cart);
            //reload the cart
            cart = _cartService.GetCart(currentUser.Id);


            RaiseEvent(NamedEvent.OrderAddressSaved, cart);
            return R.Success.Result;
        }

        [HttpGet("shipping-info", Name = RouteNames.CheckoutShippingInfo)]
        public IActionResult ShippingInfo()
        {
            if (!CanCheckout(out Cart cart))
                return RedirectToRoute(RouteNames.Home);
            //is shipping required?
            var shippingRequired = CartHelper.IsShippingRequired(cart);
            if (!shippingRequired)
            {
                return RedirectToRoute(RouteNames.CheckoutPayment);
            }
            if (cart.BillingAddressId == 0 || cart.ShippingAddressId == 0)
            {
                return R.Fail.With("error", T("The address details were not provided")).Result;
            }

            //find available shipping methods
            var shippingHandlers = _pluginAccountant.GetActivePlugins(typeof(IShipmentHandlerPlugin));
            if (!shippingHandlers.Any())
            {
                cart.ShippingMethodName = ApplicationConfig.UnavailableMethodName;
                _cartService.Update(cart);
                //there are no shipping handlers, may be it's being manually handled by store owner
                return RedirectToRoute(RouteNames.CheckoutPayment);
            }
            var shippingModels = shippingHandlers.Select(x =>
                {
                    var model = new ShippingMethodModel()
                    {
                        SystemName = x.SystemName,
                        Description = x.Description,
                        FriendlyName = x.Name
                    };
                    try
                    {
                        model.Fee = x.LoadPluginInstance<IShipmentHandlerPlugin>().GetShippingHandlerFee(cart);
                    }
                    catch(Exception ex)
                    {
                        //do nothing
                        _logger.Log<CheckoutController>(LogLevel.Error, ex.Message, ex);
                    }
                    
                    return model;
                })
                .ToList();
            return R.Success
                .With("shippingMethods", shippingModels)
                .With("shippingRequired", true)
                .Result;
        }

        /// <summary>
        /// Gets the shipping options available for selected shipping method
        /// </summary>
        /// <param name="shippingMethodSystemName">The system name of the shipping method</param>
        /// <response code="200">A list of available shipping options</response>
        [DualGet("shipping-options", Name = RouteNames.CheckoutShippingOptions)]
        public IActionResult ShippingOptions(string shippingMethodSystemName)
        {
            if (!CanCheckout(out Cart cart))
                return R.Fail.Result;
            if (cart.BillingAddressId == 0 || cart.ShippingAddressId == 0)
            {
                return R.Fail.With("error", T("The address details were not provided")).Result;
            }

            //validate shipping method
            var shippingHandler = PluginHelper.GetShipmentHandler(shippingMethodSystemName);
            if (shippingHandler == null)
                return R.Fail.With("error", T("Shipping method unavailable")).Result;

            IList<ShippingOptionModel> shippingOptionModels;
            if (cart.ShippingMethodName == shippingMethodSystemName &&
                !cart.ShippingOptionsSerialized.IsNullEmptyOrWhiteSpace())
            {
                shippingOptionModels =
                    _dataSerializer.DeserializeAs<IList<ShippingOptionModel>>(cart.ShippingOptionsSerialized);
            }
            else
            {
                shippingOptionModels = GetShipmentOptionModels(shippingHandler, cart);
                cart.ShippingOptionsSerialized = shippingOptionModels != null ? _dataSerializer.Serialize(shippingOptionModels) : null;
                //select the first option as default one
                cart.SelectedShippingOption = shippingOptionModels?.FirstOrDefault()?.Name;
                _cartService.Update(cart);

            }
            return R.Success.With("shippingOptions", shippingOptionModels).Result;
        }

        /// <summary>
        /// Saves shipping info for the cart
        /// </summary>
        /// <response code="200">A success response object</response>
        [DualPost("shipping-options", Name = RouteNames.CheckoutShippingOptions, OnlyApi = true)]
        public IActionResult ShippingInfoSave(ShippingInfoModel requestModel)
        {
            if (!CanCheckout(out Cart cart))
                return R.Fail.Result;
            var shippingRequired = CartHelper.IsShippingRequired(cart);
            if (!shippingRequired)
                return R.Success.Result;
            if (cart.BillingAddressId == 0 || cart.ShippingAddressId == 0)
            {
                return R.Fail.With("error", T("The address details were not provided")).Result;
            }
            if (cart.ShippingOptionsSerialized.IsNullEmptyOrWhiteSpace() || requestModel.ShippingOption?.Id == null)
                return R.Fail.With("error", T("No shipping options selected")).Result;

            var shippingOptionModels =
               _dataSerializer.DeserializeAs<IList<ShippingOptionModel>>(cart.ShippingOptionsSerialized);
            var shippingOptionId = requestModel.ShippingOption.Id;
            requestModel.ShippingOption = shippingOptionModels.FirstOrDefault(x => x.Id == shippingOptionId);
            if (requestModel.ShippingOption == null)
            {
                return R.Fail.With("error", T("Could not find selected shipping option")).Result;
            }
            var additionalFee = cart.CartItems.Sum(x => x.Product.AdditionalShippingCharge);
            //shipping handler validation
            if (requestModel.ShippingMethod != null)
            {
                //validate shipping method
                var shippingHandler = PluginHelper.GetShipmentHandler(requestModel.ShippingMethod.SystemName);
                if (shippingHandler == null)
                    return R.Fail.With("error", T("Shipping method unavailable")).Result;
                cart.ShippingMethodName = requestModel.ShippingMethod.SystemName;
                cart.ShippingFee = additionalFee + requestModel.ShippingOption.Rate;
                cart.ShippingMethodDisplayName = shippingHandler.PluginInfo.Name;
            }

            cart.SelectedShippingOption = $"{requestModel.ShippingOption.Name}";
            _cartService.Update(cart);
            return R.Success.Result;
        }

        [HttpGet("payment", Name = RouteNames.CheckoutPayment)]
        public IActionResult PaymentInfo(string orderGuid, bool error = false)
        {
            Order order = null;
            Cart cart = null;
            //if order guid is set, we need to make sure that order is new
            if (!orderGuid.IsNullEmptyOrWhiteSpace())
            {
                order = _orderService.GetByGuid(orderGuid);
                if (order == null || order.PaymentStatus != PaymentStatus.Pending)
                {
                    return RedirectToRoute(RouteNames.Home);
                }
            }
            else
            {
                //we don't have orderGuid, so it's a fresh checkout
                if (!CanCheckout(out cart))
                    return RedirectToRoute(RouteNames.Home);

                //is payment required
                if (!CartHelper.IsPaymentRequired(cart))
                    return RedirectToRoute(RouteNames.CheckoutConfirm);
            }

            //do we have addresses?
            if (cart != null && (cart.BillingAddressId == 0 || cart.ShippingAddressId == 0 && CartHelper.IsShippingRequired(cart)))
                return RedirectToRoute(RouteNames.CheckoutAddress);

            //find available payment methods
            var paymentHandlers = _pluginAccountant.GetActivePlugins(typeof(IPaymentHandlerPlugin));

            if (!paymentHandlers.Any())
            {
                //redirect to confirm page...payment must be handled offline
                cart.PaymentMethodName = ApplicationConfig.UnavailableMethodName;
                _cartService.Update(cart);
                return RedirectToRoute(RouteNames.CheckoutConfirm);
            }

            if ((cart != null && CartHelper.IsSubscriptionCart(cart)) || (order != null && OrderHelper.IsSubscription(order)))
            {
                //filter by payment methods which support subscription
                paymentHandlers = paymentHandlers
                    .Where(x => x.LoadPluginInstance<IPaymentHandlerPlugin>().SupportsSubscriptions).ToList();
            }
            var paymentModels = paymentHandlers.Select(x =>
                {
                    var pluginHandlerInstance = x.LoadPluginInstance<IPaymentHandlerPlugin>();
                    var model = new PaymentMethodModel()
                    {
                        SystemName = x.SystemName,
                        Description = x.Description,
                        FriendlyName = x.Name,
                        Url = Url.RouteUrl(pluginHandlerInstance.PaymentHandlerComponentRouteName)
                    };
                    try
                    {
                        model.Fee = order == null
                            ? pluginHandlerInstance.GetPaymentHandlerFee(cart)
                            : pluginHandlerInstance.GetPaymentHandlerFee(order);
                    }
                    catch(Exception ex)
                    {
                        _logger.Log<CheckoutController>(LogLevel.Error, ex.Message, ex);
                        //do nothing
                    }
                    return model;
                })
                .ToList();

            //if order guid is set, it means a payment transaction has failed in previous attempt
            //we can use this identifier to skip confirmation page for subsequent attempt
            //this will be same as retry payment from a failed order page
            return R.Success.With("paymentMethods", paymentModels)
                .With("error", error)
                .With("orderGuid", orderGuid).Result;
        }

        /// <summary>
        /// Saves the payment information for the authenticated user's cart
        /// </summary>
        /// <param name="requestModel"></param>
        /// <response code="200">A success response object</response>
        [DualPost("payment", Name = RouteNames.CheckoutPayment, OnlyApi = true)]
        [ValidateModelState(ModelType = typeof(PaymentMethodModel))]
        public IActionResult PaymentInfoSave(PaymentMethodModel requestModel)
        {
            Order order = null;
            Cart cart = null;
            if (!requestModel.OrderGuid.IsNullEmptyOrWhiteSpace())
            {
                order = _orderService.GetByGuid(requestModel.OrderGuid);
                if (order == null || order.PaymentStatus != PaymentStatus.Pending)
                {
                    return R.Fail.With("error", T("The order has been already paid")).Result;
                }
            }
            else
            {
                if (!CanCheckout(out cart))
                    return R.Fail.Result;
            }

            if (order == null && cart == null)
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

            CustomResponse response;
            if (order != null)
            {
                order.PaymentMethodName = requestModel.SystemName;
                order.PaymentMethodFee = paymentHandler.GetPaymentHandlerFee(order);
                order.OrderTotal = order.Subtotal + order.Tax + order.PaymentMethodFee ?? 0 +
                                   order.ShippingMethodFee ?? 0;
                _orderService.Update(order);

                //process the payment immediately
                ProcessPayment(order, formAsDictionary.ToDictionary(x => x.Key, x => (object)x.Value), out response);
                return response.Result;
            }
            else
            {
                //if we are here, the payment method can be saved
                cart.PaymentMethodName = requestModel.SystemName;
                cart.PaymentMethodData = _dataSerializer.Serialize(formAsDictionary);
                cart.PaymentMethodDisplayName = paymentHandler.PluginInfo.Name;
                _cartService.Update(cart);

                RaiseEvent(NamedEvent.OrderPaymentInfoSaved, cart);
            }

            response = R.Success;
            if (!requestModel.OrderGuid.IsNullEmptyOrWhiteSpace())
                response.With("orderGuid", requestModel.OrderGuid);
            return response.Result;
        }

        [HttpGet("confirm", Name = RouteNames.CheckoutConfirm)]
        public IActionResult Confirm(string orderGuid)
        {
            if (!orderGuid.IsNullEmptyOrWhiteSpace())
                return ConfirmSave();

            if (!CanCheckout(out Cart cart))
                return RedirectToRoute(RouteNames.Home);
            //do we have addresses?
            if (cart.BillingAddressId == 0 || (cart.ShippingAddressId == 0 && CartHelper.IsShippingRequired(cart)))
                return RedirectToRoute(RouteNames.CheckoutAddress);
            if (CartHelper.IsPaymentRequired(cart) && cart.PaymentMethodName.IsNullEmptyOrWhiteSpace())
            {
                return RedirectToRoute(RouteNames.CheckoutAddress);
            }
            return R.Success.Result;
        }

        /// <summary>
        /// Completes the checkout process
        /// </summary>
        /// <response code="200">Depending on the payment method type, user may be returned with a url to complete the payment with redirection. Returns a unique orderGuid otherwise.</response>
        [DualPost("confirm", Name = RouteNames.CheckoutConfirm, OnlyApi = true)]
        public IActionResult ConfirmSave()
        {
            if (!CanCheckout(out Cart cart))
                return R.Fail.With("error", T("An error occurred while checking out")).Result;
            if (cart.BillingAddressId == 0 ||
                ((cart.ShippingAddressId == 0 || cart.ShippingMethodName.IsNullEmptyOrWhiteSpace()) &&
                 CartHelper.IsShippingRequired(cart)))
            {
                return R.Fail.With("error", T("The address details were not provided")).Result;
            }
            if (CartHelper.IsPaymentRequired(cart) && cart.PaymentMethodName.IsNullEmptyOrWhiteSpace())
            {
                return R.Fail.With("error", T("The payment method was not provided")).Result;
            }

            var currentUser = ApplicationEngine.CurrentUser;
            var order = new Order()
            {
                PaymentMethodName = cart.PaymentMethodName,
                PaymentMethodDisplayName = cart.PaymentMethodDisplayName,
                ShippingMethodName = cart.ShippingMethodName,
                ShippingMethodDisplayName = cart.ShippingMethodDisplayName,
                SelectedShippingOption = cart.SelectedShippingOption,
                CreatedOn = DateTime.UtcNow,
                DiscountId = cart.DiscountCouponId,
                Guid = Guid.NewGuid().ToString(),
                UserId = cart.UserId,
                PaymentStatus = PaymentStatus.Pending,
                OrderStatus = OrderStatus.New,
                DiscountCoupon = cart.DiscountCoupon?.CouponCode,
                Discount = cart.Discount + cart.CartItems.Sum(x => x.Discount),
                PaymentMethodFee = cart.PaymentMethodFee,
                ShippingMethodFee = cart.ShippingFee,
                Tax = cart.CartItems.Sum(x => x.Tax),
                UserIpAddress = WebHelper.GetClientIpAddress(),
                CurrencyCode = ApplicationEngine.BaseCurrency.IsoCode,
                Subtotal = cart.FinalAmount - cart.CartItems.Sum(x => x.Tax),
                ExchangeRate = ApplicationEngine.BaseCurrency.ExchangeRate,
                DisableReturns = cart.CartItems.All(x => !x.Product.AllowReturns),
                User = currentUser,
                IsSubscription = CartHelper.IsSubscriptionCart(cart)
            };
            order.OrderTotal = order.Subtotal + order.Tax + order.PaymentMethodFee ?? 0 +
                               order.ShippingMethodFee ?? 0;
            //load the addresses
            var addressIds = new List<int>() { cart.BillingAddressId, cart.ShippingAddressId };
            var addresses = _addressService.Get(x => addressIds.Contains(x.Id)).ToList();
            var billingAddress = addresses.First(x => x.Id == cart.BillingAddressId);
            var shippingAddress = addresses.FirstOrDefault(x => x.Id == cart.ShippingAddressId);
            order.BillingAddressSerialized = _dataSerializer.Serialize(billingAddress);
            order.ShippingAddressSerialized =
                shippingAddress == null ? null : _dataSerializer.Serialize(shippingAddress);

            //get all the products
            var distinctProductIds = cart.CartItems.Select(x => x.ProductId).ToList();
            var products = _productService.Get(x => distinctProductIds.Contains(x.Id)).ToList();
            order.OrderItems = new List<OrderItem>();
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
                    TaxPercent = cartItem.TaxPercent,
                    TaxName = cartItem.TaxName,
                    ProductVariantId = cartItem.ProductVariantId,
                    IsDownloadable = cartItem.IsDownloadable,
                    Product = products.First(x => x.Id == cartItem.ProductId),
                };
                orderItem.SubscriptionCycle = orderItem.Product.SubscriptionCycle;
                orderItem.CycleCount = orderItem.Product.CycleCount;
                orderItem.TrialDays = orderItem.Product.TrialDays;
                order.OrderItems.Add(orderItem);
            }
            //insert complete order
            _orderAccountant.InsertCompleteOrder(order);

            //process payment
            var paymentMethodData = cart.PaymentMethodData.IsNullEmptyOrWhiteSpace()
                ? null
                : _dataSerializer.DeserializeAs<Dictionary<string, object>>(cart.PaymentMethodData);
            CustomResponse response = null;
            if (cart.PaymentMethodName != ApplicationConfig.UnavailableMethodName && !ProcessPayment(order, paymentMethodData, out response))
            {
                return response.With("orderGuid", order.Guid).Result;
            }

            //clear the user's cart
            _cartService.ClearCart(currentUser.Id);

            if (currentUser.IsVisitor())
            {
                //if current user is visitor, change the email to billing address and change it to registered user
                currentUser.Email = billingAddress.Email;
                _userService.Update(currentUser);

                var roleId = _roleService.Get(x => x.SystemName == SystemRoleNames.Registered).First().Id;
                //assign registered role to the user
                _roleService.SetUserRoles(currentUser.Id, new[] { roleId });

                ApplicationEngine.SignIn(currentUser.Email, null, false);
            }

            response = response ?? R.Success;
            return response.With("orderGuid", order.Guid).Result;
        }

        [HttpGet("complete/{orderGuid}", Name = RouteNames.CheckoutComplete)]
        public IActionResult Complete(string orderGuid)
        {
            var order = _orderService.GetByGuid(orderGuid);
            if (order == null || order.UserId != ApplicationEngine.CurrentUser.Id)
                return NotFound();
            //initialize downloads
            _downloadService.InitializeDownloads(order);

            RaiseEvent(NamedEvent.OrderPlaced, order.User, order);
            return R.Success.With("orderGuid", orderGuid).With("orderNumber", order.OrderNumber).Result;
        }

        [HttpGet("fail/{orderGuid}", Name = RouteNames.CheckoutFailed)]
        public IActionResult Failed(string orderGuid)
        {
            var order = _orderService.GetByGuid(orderGuid);
            if (order == null || order.UserId != ApplicationEngine.CurrentUser.Id)
                return NotFound();
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

            if (CartHelper.HasConflictingProducts(cart))
                return false;

            //do we have any items remaining
            return cart.CartItems.Any();
        }

        private bool ProcessPayment(Order order, Dictionary<string, object> paymentMethodData, out CustomResponse response)
        {
            var isSubscription = OrderHelper.IsSubscription(order);
            var transactionResult = isSubscription
                ? _paymentProcessor.ProcessCreateSubscription(order, paymentMethodData)
                : _paymentProcessor.ProcessPayment(order, paymentMethodData);

            if (transactionResult.Success)
            {
                response = R.Success;

                if (transactionResult.RequiresRedirection)
                    response.Redirect(transactionResult.RedirectionUrl);
                else
                    //if we are here, payment has been done, so we can get the transaction data
                    //create payment transaction object and save it to database
                    _paymentAccountant.ProcessTransactionResult(transactionResult);
                return true;
            }

            response = R.Fail.With("error", T("An error occurred while checking out"));
            return false;
        }

        private IList<ShippingOptionModel> GetShipmentOptionModels(IShipmentHandlerPlugin shipmentHandler, Cart cart)
        {
            //find the product ids of the cart
            var productIds = cart.CartItems.Select(x => x.ProductId).ToList();
            var products = _productService.GetProductsWithVariants(productIds);
            var warehouseWiseProducts = new Dictionary<Warehouse, IList<Product>>();
            foreach (var item in cart.CartItems)
            {
                var product = products.First(x => x.Id == item.ProductId);
                Warehouse warehouse;
                if (item.ProductVariantId > 0)
                {
                    var productVariant = product.ProductVariants.First(x => x.Id == item.ProductVariantId);
                    if (!productVariant.IsAvailableInStock(product, out warehouse))
                        continue;
                }
                else
                {
                    if (!product.IsAvailableInStock(out warehouse))
                        continue;
                }
                if (warehouseWiseProducts.All(x => x.Key.Id != warehouse.Id))
                {
                    warehouseWiseProducts.Add(warehouse, new List<Product>());
                }
                warehouseWiseProducts[warehouse].Add(product);
            }
            var shippingOptionModels = new List<ShippingOptionModel>();
            //if there are more than two warehouses, we'll need to calculate shipping for each warehouse
            foreach (var warehousePair in warehouseWiseProducts)
            {
                var shipper = warehousePair.Key.Address;
                var warehouseProducts = warehousePair.Value;
                var productsThatCanBeShippedTogether = warehouseProducts.Where(x => !x.IndividuallyShipped).ToList();
                var productsThatShouldBeShippedIndividually =
                    warehouseProducts.Where(x => x.IndividuallyShipped).ToList();

                var shippingOptions = shipmentHandler.GetAvailableOptions(productsThatCanBeShippedTogether, shipper, cart.ShippingAddress);
                //set unique ids
                foreach (var so in shippingOptions)
                    so.Id = Guid.NewGuid().ToString();
                shippingOptionModels = shippingOptionModels.Concat(shippingOptions.Select(x => _modelMapper.Map<ShippingOptionModel>(x))).ToList();
            }
            return shippingOptionModels;
        }
        #endregion

    }
}