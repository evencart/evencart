using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoastedMarketplace.Data.Entity.Addresses;
using RoastedMarketplace.Data.Entity.Purchases;
using RoastedMarketplace.Factories.Orders;
using RoastedMarketplace.Infrastructure;
using RoastedMarketplace.Infrastructure.Mvc;
using RoastedMarketplace.Infrastructure.Mvc.ModelFactories;
using RoastedMarketplace.Infrastructure.Routing;
using RoastedMarketplace.Infrastructure.ViewEngines.GlobalObjects;
using RoastedMarketplace.Models.Addresses;
using RoastedMarketplace.Models.Orders;
using RoastedMarketplace.Models.Users;
using RoastedMarketplace.Services.Addresses;
using RoastedMarketplace.Services.Purchases;
using RoastedMarketplace.Services.Reviews;
using RoastedMarketplace.Services.Users;

namespace RoastedMarketplace.Controllers
{
    [Route("account")]
    [Authorize]
    public class AccountController : FoundationController
    {
        private readonly IUserService _userService;
        private readonly IOrderService _orderService;
        private readonly IAddressService _addressService;
        private readonly ICartService _cartService;
        private readonly IReviewService _reviewService;
        private readonly IModelMapper _modelMapper;
        private readonly IOrderModelFactory _orderModelFactory;

        public AccountController(IUserService userService, IOrderService orderService, IAddressService addressService, ICartService cartService, IReviewService reviewService, IModelMapper modelMapper, IOrderModelFactory orderModelFactory)
        {
            _userService = userService;
            _orderService = orderService;
            _addressService = addressService;
            _cartService = cartService;
            _reviewService = reviewService;
            _modelMapper = modelMapper;
            _orderModelFactory = orderModelFactory;
        }

        [DualGet("", Name = RouteNames.AccountProfile)]
        public IActionResult Profile()
        {
            var currentUser = ApplicationEngine.CurrentUser;
            var userModel = _modelMapper.Map<UserModel>(currentUser);
            return R.Success.With("user", userModel).Result;
        }

        [DualGet("orders", Name = RouteNames.AccountOrders)]
        public IActionResult Orders(OrderSearchModel searchModel)
        {
            var orderStatus = new List<OrderStatus>();
            switch (searchModel.OrderStatus)
            {
                case "closed":
                    orderStatus.Add(OrderStatus.Closed);
                    orderStatus.Add(OrderStatus.Complete);
                    break;
                case "open":
                    orderStatus.Add(OrderStatus.New);
                    orderStatus.Add(OrderStatus.OnHold);
                    orderStatus.Add(OrderStatus.Shipped);
                    orderStatus.Add(OrderStatus.Processing);
                    orderStatus.Add(OrderStatus.Delayed);
                    orderStatus.Add(OrderStatus.PartiallyShipped);
                    break;
                case "returned":
                    orderStatus.Add(OrderStatus.Returned);
                    break;
                case "cancelled":
                    orderStatus.Add(OrderStatus.Cancelled);
                    break;
                default:
                    break;//do nothing we'll show all orders by default
            }

            var currentUser = ApplicationEngine.CurrentUser;
            var orders = _orderService.GetOrders(out int totalResults, userId: currentUser.Id, startDate: searchModel.FromDate, endDate: searchModel.ToDate,
                orderStatus: orderStatus, page: searchModel.Current, count: searchModel.RowCount);
            var orderModels = orders.Select(x => _orderModelFactory.Create(x)).ToList();

            return R.Success.With("orders", orderModels).Result;
        }

        [DualGet("addresses", Name = RouteNames.AccountAddresses)]
        public IActionResult Addresses()
        {
            var currentUser = ApplicationEngine.CurrentUser;
            var addresses = _addressService.Get(x => x.UserId == currentUser.Id).ToList();
            var models = addresses.Select(x =>
            {
                var model = _modelMapper.Map<AddressInfoModel>(x);
                model.CountryName = x.Country.Name;
                model.StateProvinceName = x.StateOrProvince?.Name ?? x.StateProvinceName;
                return model;
            }).ToList();
            return R.Success.With("addresses", models).Result;
        }

        [DualGet("addresses/{addressId}", Name = RouteNames.SingleAddress)]
        public IActionResult AddressEditor(int addressId)
        {
            var currentUser = ApplicationEngine.CurrentUser;
            //find address
            var address = addressId > 0 ? _addressService.Get(addressId) : new Address()
            {
                UserId = currentUser.Id
            };
            //only allow if current user can edit this address
            if (address == null || address.UserId != ApplicationEngine.CurrentUser.Id)
                return NotFound();
            var model = _modelMapper.Map<AddressInfoModel>(address);
            return R.Success.With("address", model).WithAvailableCountries().WithAvailableAddressTypes().Result;
        }

        [DualGet("wishlist", Name = RouteNames.AccountWishlist)]
        public IActionResult WishList()
        {
            var wishList = new CartObject(true).GetObject();
            return R.Success.With("wishlist", wishList).Result;
        }
    }
}