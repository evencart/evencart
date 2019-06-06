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
using EvenCart.Data.Extensions;
using EvenCart.Services.Addresses;
using EvenCart.Services.Extensions;
using EvenCart.Services.Helpers;
using EvenCart.Services.Payments;
using EvenCart.Services.Plugins;
using EvenCart.Services.Purchases;
using EvenCart.Services.Serializers;
using EvenCart.Services.Tokens;
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
        private readonly IOrderItemService _orderItemService;
        private readonly OrderSettings _orderSettings;
        private readonly IRoleService _roleService;
        private readonly IUserService _userService;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly IProductService _productService;
        public CheckoutController(IPaymentProcessor paymentProcessor, IPaymentAccountant paymentAccountant, IModelMapper modelMapper, IAddressService addressService, ICartService cartService, IDataSerializer dataSerializer, IPluginAccountant pluginAccountant, IOrderService orderService, IOrderItemService orderItemService, OrderSettings orderSettings, IRoleService roleService, IUserService userService, ITokenGenerator tokenGenerator, IProductService productService)
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
            _productService = productService;
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
                if (_addressService.Count(x => x.EntityId == currentUser.Id && x.Id == billingAddressId) == 0)
                    return R.Fail.With("error", T("Invalid billing address provided")).Result;
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
            _cartService.Update(cart);
            //reload the cart
            cart = _cartService.GetCart(currentUser.Id);
            //shipping handler validation
            if (requestModel.ShippingMethod != null)
            {
                //validate shipping method
                var shippingHandler = PluginHelper.GetShipmentHandler(requestModel.ShippingMethod.SystemName);
                if (shippingHandler == null)
                    return R.Fail.With("error", T("Shipping method unavailable")).Result;
                cart.ShippingMethodName = requestModel.ShippingMethod.SystemName;
                cart.ShippingFee = shippingHandler.GetShippingHandlerFee(cart);
                cart.ShippingMethodDisplayName = shippingHandler.PluginInfo.Name;
                //find the shipping options & store shipping options. These 
                var shippingOptionModels = GetShipmentOptionModels(shippingHandler, cart);
                cart.ShippingOptionsSerialized = shippingOptionModels != null ? _dataSerializer.Serialize(shippingOptionModels) : "[]";
                //select the first option as default one
                cart.SelectedShippingOption = shippingOptionModels?.FirstOrDefault()?.Name;
            }

         

            RaiseEvent(NamedEvent.OrderAddressSaved, cart);
            return R.Success.Result;
        }

        /// <summary>
        /// Gets the shipping options available for selected shipping method
        /// </summary>
        /// <param name="shippingMethodSystemName">The system name of the shipping method</param>
        /// <response code="200">A list of available shipping options</response>
        [DualGet("shipping-options", Name = RouteNames.CheckoutShippingOption)]
        public IActionResult ShippingOptions(string shippingMethodSystemName)
        {
            if (!CanCheckout(out Cart cart))
                return R.Fail.Result;
            //validate shipping method
            var shippingHandler = PluginHelper.GetShipmentHandler(shippingMethodSystemName);
            if (shippingHandler == null)
                return R.Fail.With("error", T("Shipping method unavailable")).Result;
            var shippingOptionModels = GetShipmentOptionModels(shippingHandler, cart);
            return R.Success.With("shippingOptions", shippingOptionModels).Result;
        }

        /// <summary>
        /// Saves shipping option for the cart
        /// </summary>
        /// <response code="200">A success response object</response>
        [DualPost("shipping-options", Name = RouteNames.CheckoutShippingOption)]
        public IActionResult ShippingOptionsSave(string shippingOptionId)
        {
            if (!CanCheckout(out Cart cart))
                return R.Fail.Result;
            if (cart.ShippingOptionsSerialized.IsNullEmptyOrWhiteSpace())
                return R.Fail.With("error", T("No shipping options found")).Result;

            var shippingOptionModels =
                _dataSerializer.DeserializeAs<IList<ShippingOptionModel>>(cart.ShippingOptionsSerialized);

            var selectedOption = shippingOptionModels.FirstOrDefault(x => x.Id == shippingOptionId);
            if(selectedOption == null)
                return R.Fail.With("error", T("Unknown shipping option")).Result;
            cart.SelectedShippingOption = $"{selectedOption.Name} - {selectedOption.DeliveryTime}";
            cart.ShippingFee = selectedOption.Rate;
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
                        Fee = order == null
                            ? pluginHandlerInstance.GetPaymentHandlerFee(cart)
                            : pluginHandlerInstance.GetPaymentHandlerFee(order),
                        Url = Url.RouteUrl(pluginHandlerInstance.PaymentHandlerComponentRouteName)
                    };
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
            if (cart == null)
            {
                order.PaymentMethodName = requestModel.SystemName;
                order.PaymentMethodFee = paymentHandler.GetPaymentHandlerFee(order);
                order.OrderTotal = order.Subtotal + order.Tax + order.PaymentMethodFee ?? 0 +
                                   order.ShippingMethodFee ?? 0;
                _orderService.Update(order);

                //process the payment immediately
                ProcessPayment(order, formAsDictionary.ToDictionary(x => x.Key, x => (object) x.Value), out response);
                return response.Result;
            }
            else
            {
                //if we are here, the payment method can be saved
                cart.PaymentMethodName = requestModel.SystemName;
                cart.PaymentMethodData = _dataSerializer.Serialize(formAsDictionary);
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

            var currentUser = ApplicationEngine.CurrentUser;
            var order = new Order()
            {
                PaymentMethodName = cart.PaymentMethodName,
                ShippingMethodName = cart.ShippingMethodName,
                CreatedOn = DateTime.UtcNow,
                DiscountId = cart.DiscountCouponId,
                Guid = Guid.NewGuid().ToString(),
                UserId = cart.UserId,
                PaymentStatus = PaymentStatus.Pending,
                OrderStatus = OrderStatus.New,
                DiscountCoupon =  cart.DiscountCoupon?.CouponCode,
                Discount = cart.Discount + cart.CartItems.Sum(x => x.Discount),
                PaymentMethodFee = cart.PaymentMethodFee,
                ShippingMethodFee = cart.ShippingFee,
                Tax = cart.CartItems.Sum(x => x.Tax),
                UserIpAddress = WebHelper.GetClientIpAddress(),
                CurrencyCode = ApplicationEngine.BaseCurrency.IsoCode,
                Subtotal = cart.FinalAmount - cart.CartItems.Sum(x => x.Tax),
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
                    TaxPercent = cartItem.TaxPercent,
                    ProductVariantId = cartItem.ProductVariantId
                };
                orderItems.Add(orderItem);
            }
            //save the order items
            _orderItemService.Insert(orderItems.ToArray());

            //process payment
            var paymentMethodData = cart.PaymentMethodData.IsNullEmptyOrWhiteSpace()
                ? null
                : _dataSerializer.DeserializeAs<Dictionary<string, object>>(cart.PaymentMethodData);
            
            if (!ProcessPayment(order, paymentMethodData, out CustomResponse response))
            {
                return response.Result;
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

            return response.With("orderGuid", order.Guid).Result;
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

        private bool ProcessPayment(Order order, Dictionary<string, object> paymentMethodData, out CustomResponse response)
        {
            var transactionResult = _paymentProcessor.ProcessPayment(order, paymentMethodData);
            
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