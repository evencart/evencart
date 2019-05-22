using System;
using System.Collections.Generic;
using System.Linq;
using EvenCart.Data.Entity.Promotions;
using EvenCart.Data.Entity.Purchases;
using EvenCart.Data.Entity.Shop;
using EvenCart.Data.Entity.Users;
using EvenCart.Services.Helpers;
using EvenCart.Services.Products;
using EvenCart.Services.Promotions;
using EvenCart.Services.Purchases;
using EvenCart.Services.Users;
using NUnit.Framework;

namespace EvenCart.Services.Tests.Purchases
{
    public abstract class PriceAccountantTests : BaseTest
    {
        private IRoleService _roleService;
        private ICartService _cartService;
        private IUserService _userService;
        private IProductService _productService;
        private ICategoryService _categoryService;
        private IPriceAccountant _priceAccountant;
        private IManufacturerService _manufacturerService;
        private IVendorService _vendorService;
        private IDiscountCouponService _discountCouponService;

        private User _registeredUser;
        private User _visitor;
        private Product _product1, _product2;
        private Role _registeredRole, _visitorRole;
        private Vendor _vendor;
        private Manufacturer _manufacturer;
        private Category _category;
        private DiscountCoupon _autoCoupon;
        private DiscountCoupon _validCoupon;

        [OneTimeSetUp]
        protected void Setup()
        {
            _cartService = Resolve<ICartService>();
            _userService = Resolve<IUserService>();
            _productService = Resolve<IProductService>();
            _priceAccountant = Resolve<IPriceAccountant>();
            _roleService = Resolve<IRoleService>();
            _discountCouponService = Resolve<IDiscountCouponService>();
            _categoryService = Resolve<ICategoryService>();
            _vendorService = Resolve<IVendorService>();
            _manufacturerService = Resolve<IManufacturerService>();

            _registeredUser = new User()
            {
                Email = "priceaccountanttests_registered@teststore.com",
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow,
            };
            _userService.Insert(_registeredUser);
            _registeredRole = _roleService.FirstOrDefault(x => x.SystemName == SystemRoleNames.Registered);
            _roleService.SetUserRoles(_registeredUser.Id, new [] { _registeredRole.Id }); //registered user
            _registeredUser = _userService.Get(_registeredUser.Id);

            _visitor = new User()
            {
                Email = "priceaccountanttests_visitor@teststore.com",
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow,
            };

            _userService.Insert(_visitor);
            _visitorRole = _roleService.FirstOrDefault(x => x.SystemName == SystemRoleNames.Visitor);
            _roleService.SetUserRoles(_visitor.Id, new[] { _visitorRole.Id }); //visitor user
            _visitor = _userService.Get(_visitor.Id);

            _manufacturer = new Manufacturer()
            {
                Name = "Test Manufacturer"
            };
            _manufacturerService.Insert(_manufacturer);

            _vendor = new Vendor()
            {
                Name = "Test Vendor",
                Address = "Test Address",
                City = "Test City"
            };
            _vendorService.Insert(_vendor);
            
            var products = EvenCart.Tests.Data.Products.GetList();
            _product1 = products[0];
            _product1.ManufacturerId = _manufacturer.Id;
            _productService.Insert(_product1);

            _product2 = products[1];
            _productService.Insert(_product2);
            _vendorService.AddVendorProduct(_vendor.Id, _product2.Id);

            _category = new Category()
            {
                Name = "Test Category"
            };
            _categoryService.Insert(_category);

            _productService.LinkCategoryWithProduct(_category.Id, _product1.Id, 0);

           

            var discounts = new[]
            {
                new DiscountCoupon()
                {
                    Name = "Test Coupon One",
                    CalculationType = CalculationType.FixedAmount,
                    HasCouponCode = false,
                    DiscountValue = 10,
                    Enabled = true,
                    StartDate = DateTime.UtcNow,
                    EndDate = DateTime.UtcNow.AddDays(5),
                    NumberOfTimesPerUser = 1,
                    TotalNumberOfTimes = 5,
                    RestrictionType = RestrictionType.OrderTotal
                },
                new DiscountCoupon()
                {
                    Name = "Test Coupon Two",
                    CalculationType = CalculationType.Percentage,
                    CouponCode = "VALIDCOUPON",
                    HasCouponCode = true,
                    DiscountValue = 5,
                    Enabled = true,
                    StartDate = DateTime.UtcNow,
                    EndDate = DateTime.UtcNow.AddDays(5),
                    NumberOfTimesPerUser = 1,
                    TotalNumberOfTimes = 5
                },
                new DiscountCoupon()
                {
                    Name = "Expired Coupon",
                    CalculationType = CalculationType.Percentage,
                    HasCouponCode = true,
                    CouponCode = "EXPIREDCOUPON",
                    DiscountValue = 5,
                    Enabled = true,
                    StartDate = DateTime.UtcNow.AddDays(-5),
                    EndDate = DateTime.UtcNow.AddDays(-1),
                    NumberOfTimesPerUser = 1,
                    TotalNumberOfTimes = 5,
                    MaximumDiscountAmount = 10
                },
            };
            //save some discounts
            _discountCouponService.Insert(discounts);
            _autoCoupon = discounts[0];
            _validCoupon = discounts[1];
        }

        [Test]
        public void Invalid_Discount_Application_Fails()
        {
            var cart = _cartService.GetCart(_registeredUser.Id);
            var result = _priceAccountant.ApplyDiscountCoupon("SOME-RANDOM-COUPON-THAT-DOESN'T-EXIST", cart);
            Assert.AreEqual(DiscountApplicationStatus.InvalidCode, result);
        }

        [Test]
        public void Expired_Discount_Application_Fails()
        {
            var cart = _cartService.GetCart(_registeredUser.Id);
            var result = _priceAccountant.ApplyDiscountCoupon("expiredcoupon", cart);
            Assert.AreEqual(DiscountApplicationStatus.Expired, result);
        }

        [Test]
        public void Auto_Discount_OrderTotal_Succeeds()
        {
            _autoCoupon.RestrictionType = RestrictionType.OrderTotal;
            _autoCoupon.MaximumDiscountAmount = 0;
            _discountCouponService.Update(_autoCoupon);

            //clear cart
            _cartService.ClearCart(_registeredUser.Id);

            //add items
            _cartService.AddToCart(_registeredUser.Id, new CartItem()
            {
                ProductId = _product1.Id,
                Quantity = 1,
                Discount = 0,
                Price = _product1.Price,
                Tax = 0,
                TaxPercent = 0,
                FinalPrice = _product1.Price
            });
            var cart = _cartService.GetCart(_registeredUser.Id);
            _priceAccountant.RefreshCartParameters(cart);
            Assert.AreEqual(_autoCoupon.DiscountValue, cart.Discount);
        }

        [Test]
        public void Auto_Discount_Product_Succeeds()
        {
            _autoCoupon.RestrictionType = RestrictionType.Products;
            _autoCoupon.Enabled = true;
            _autoCoupon.MaximumDiscountAmount = 0;
            _discountCouponService.Update(_autoCoupon);

            _discountCouponService.SetRestrictionIdentifiers(_autoCoupon.Id, new List<string>() { _product2.Id.ToString() });

            //clear cart
            _cartService.ClearCart(_registeredUser.Id);
            //add items
            _cartService.AddToCart(_registeredUser.Id, new CartItem()
            {
                ProductId = _product1.Id,
                Quantity = 1,
                Discount = 0,
                Price = _product1.Price,
                Tax = 0,
                TaxPercent = 0,
                FinalPrice = _product1.Price
            });

            var cart = _cartService.GetCart(_registeredUser.Id);
            _priceAccountant.RefreshCartParameters(cart);
            
            Assert.AreEqual(0, cart.Discount);

            _cartService.AddToCart(_registeredUser.Id, new CartItem()
            {
                ProductId = _product2.Id,
                Quantity = 2,
                Discount = 0,
                Price = _product2.Price,
                Tax = 0,
                TaxPercent = 0,
                FinalPrice = _product2.Price
            });
            cart = _cartService.GetCart(_registeredUser.Id);
            _priceAccountant.RefreshCartParameters(cart);
            //discount should be applied on both items of product2
            Assert.AreEqual(0, cart.Discount);
            Assert.AreEqual(0, cart.CartItems[0].Discount);
            Assert.AreEqual(20, cart.CartItems[1].Discount);

            _autoCoupon.MaximumDiscountAmount = 10;
            _discountCouponService.Update(_autoCoupon);
            _priceAccountant.RefreshCartParameters(cart);
            //discount should be applied on both items of product2 restricted to max
            Assert.AreEqual(0, cart.Discount);
            Assert.AreEqual(0, cart.CartItems[0].Discount);
            Assert.AreEqual(10, cart.CartItems[1].Discount);

        }

        [Test]
        public void Auto_Discount_Category_Succeeds()
        {
            _autoCoupon.RestrictionType = RestrictionType.Categories;
            _autoCoupon.MaximumDiscountAmount = 10;
            _discountCouponService.Update(_autoCoupon);

            _discountCouponService.SetRestrictionIdentifiers(_autoCoupon.Id, new List<string>() { _category.Id.ToString() });

            //clear cart
            _cartService.ClearCart(_registeredUser.Id);
            //add items
            _cartService.AddToCart(_registeredUser.Id, new CartItem()
            {
                ProductId = _product1.Id,
                Quantity = 1,
                Discount = 0,
                Price = _product1.Price,
                Tax = 0,
                TaxPercent = 0,
                FinalPrice = _product1.Price
            });

            var cart = _cartService.GetCart(_registeredUser.Id);
            _priceAccountant.RefreshCartParameters(cart);
            
            Assert.AreEqual(0, cart.Discount);
            Assert.AreEqual(10, cart.CartItems[0].Discount);

            _cartService.AddToCart(_registeredUser.Id, new CartItem()
            {
                ProductId = _product2.Id,
                Quantity = 2,
                Discount = 0,
                Price = _product2.Price,
                Tax = 0,
                TaxPercent = 0,
                FinalPrice = _product2.Price
            });
            cart = _cartService.GetCart(_registeredUser.Id);
            _priceAccountant.RefreshCartParameters(cart);
            //discount should be applied on both items of product2
            Assert.AreEqual(0, cart.Discount);
            Assert.AreEqual(10, cart.CartItems[0].Discount);
            Assert.AreEqual(0, cart.CartItems[1].Discount);

        }

        [Test]
        public void Auto_Discount_Role_Succeeds()
        {
            _autoCoupon.RestrictionType = RestrictionType.Roles;
            _autoCoupon.MaximumDiscountAmount = 10;
            _discountCouponService.Update(_autoCoupon);

            _discountCouponService.SetRestrictionIdentifiers(_autoCoupon.Id, new List<string>() { _registeredRole.Id.ToString() });

            //clear registered user cart
            _cartService.ClearCart(_registeredUser.Id);
            //add items
            _cartService.AddToCart(_registeredUser.Id, new CartItem()
            {
                ProductId = _product1.Id,
                Quantity = 2,
                Discount = 0,
                Price = _product1.Price,
                Tax = 0,
                TaxPercent = 0,
                FinalPrice = _product1.Price
            });

            var rCart = _cartService.GetCart(_registeredUser.Id);
            _priceAccountant.RefreshCartParameters(rCart);
           
            Assert.AreEqual(0, rCart.Discount);
            Assert.AreEqual(10, rCart.CartItems[0].Discount);
            //clear registered user cart
            _cartService.ClearCart(_visitor.Id);
            //add items
            _cartService.AddToCart(_visitor.Id, new CartItem()
            {
                ProductId = _product1.Id,
                Quantity = 2,
                Discount = 0,
                Price = _product1.Price,
                Tax = 0,
                TaxPercent = 0,
                FinalPrice = _product1.Price
            });

            var vCart = _cartService.GetCart(_visitor.Id);
            _priceAccountant.RefreshCartParameters(vCart);

            Assert.AreEqual(0, vCart.Discount);
            Assert.AreEqual(0, vCart.CartItems[0].Discount);
        }

        [Test]
        public void Auto_Discount_User_Succeeds()
        {
            _autoCoupon.RestrictionType = RestrictionType.Users;
            _autoCoupon.MaximumDiscountAmount = 10;
            _discountCouponService.Update(_autoCoupon);

            _discountCouponService.SetRestrictionIdentifiers(_autoCoupon.Id, new List<string>() { _registeredUser.Id.ToString() });

            //clear registered user cart
            _cartService.ClearCart(_registeredUser.Id);
            //add items
            _cartService.AddToCart(_registeredUser.Id, new CartItem()
            {
                ProductId = _product1.Id,
                Quantity = 2,
                Discount = 0,
                Price = _product1.Price,
                Tax = 0,
                TaxPercent = 0,
                FinalPrice = _product1.Price
            });

            var rCart = _cartService.GetCart(_registeredUser.Id);
            _priceAccountant.RefreshCartParameters(rCart);

            Assert.AreEqual(0, rCart.Discount);
            Assert.AreEqual(10, rCart.CartItems[0].Discount);
            //clear registered user cart
            _cartService.ClearCart(_visitor.Id);
            //add items
            _cartService.AddToCart(_visitor.Id, new CartItem()
            {
                ProductId = _product1.Id,
                Quantity = 2,
                Discount = 0,
                Price = _product1.Price,
                Tax = 0,
                TaxPercent = 0,
                FinalPrice = _product1.Price
            });

            var vCart = _cartService.GetCart(_visitor.Id);
            _priceAccountant.RefreshCartParameters(vCart);

            Assert.AreEqual(0, vCart.Discount);
            Assert.AreEqual(0, vCart.CartItems[0].Discount);
        }

        [Test]
        public void Auto_Discount_Manufacturer_Succeeds()
        {
            _autoCoupon.RestrictionType = RestrictionType.Manufacturers;
            _autoCoupon.MaximumDiscountAmount = 10;
            _discountCouponService.Update(_autoCoupon);

            _discountCouponService.SetRestrictionIdentifiers(_autoCoupon.Id, new List<string>() { _manufacturer.Id.ToString() });

            //clear registered user cart
            _cartService.ClearCart(_registeredUser.Id);
            //add items
            _cartService.AddToCart(_registeredUser.Id, new CartItem()
            {
                ProductId = _product1.Id,
                Quantity = 2,
                Discount = 0,
                Price = _product1.Price,
                Tax = 0,
                TaxPercent = 0,
                FinalPrice = _product1.Price
            });

            _cartService.AddToCart(_registeredUser.Id, new CartItem()
            {
                ProductId = _product2.Id,
                Quantity = 2,
                Discount = 0,
                Price = _product2.Price,
                Tax = 0,
                TaxPercent = 0,
                FinalPrice = _product2.Price
            });

            var rCart = _cartService.GetCart(_registeredUser.Id);
            _priceAccountant.RefreshCartParameters(rCart);

            Assert.AreEqual(0, rCart.Discount);
            Assert.AreEqual(10, rCart.CartItems[0].Discount);
            Assert.AreEqual(0, rCart.CartItems[1].Discount);
        }

        [Test]
        public void Auto_Discount_Vendor_Succeeds()
        {
            _autoCoupon.RestrictionType = RestrictionType.Vendors;
            _autoCoupon.MaximumDiscountAmount = 10;
            _discountCouponService.Update(_autoCoupon);

            _discountCouponService.SetRestrictionIdentifiers(_autoCoupon.Id, new List<string>() { _vendor.Id.ToString() });

            //clear registered user cart
            _cartService.ClearCart(_registeredUser.Id);
            //add items
            _cartService.AddToCart(_registeredUser.Id, new CartItem()
            {
                ProductId = _product1.Id,
                Quantity = 2,
                Discount = 0,
                Price = _product1.Price,
                Tax = 0,
                TaxPercent = 0,
                FinalPrice = _product1.Price
            });

            _cartService.AddToCart(_registeredUser.Id, new CartItem()
            {
                ProductId = _product2.Id,
                Quantity = 2,
                Discount = 0,
                Price = _product2.Price,
                Tax = 0,
                TaxPercent = 0,
                FinalPrice = _product2.Price
            });

            var rCart = _cartService.GetCart(_registeredUser.Id);
            _priceAccountant.RefreshCartParameters(rCart);

            Assert.AreEqual(0, rCart.Discount);
            Assert.AreEqual(0, rCart.CartItems[0].Discount);
            Assert.AreEqual(10, rCart.CartItems[1].Discount);
        }

        [Test]
        public void Manual_Discount_OrderTotal_Succeeds()
        {
            _validCoupon.RestrictionType = RestrictionType.OrderTotal;
            _discountCouponService.Update(_validCoupon);

            //clear cart
            _cartService.ClearCart(_registeredUser.Id);

            //add items
            _cartService.AddToCart(_registeredUser.Id, new CartItem()
            {
                ProductId = _product1.Id,
                Quantity = 1,
                Discount = 0,
                Price = _product1.Price,
                Tax = 0,
                TaxPercent = 0,
                FinalPrice = _product1.Price
            });
            _cartService.AddToCart(_registeredUser.Id, new CartItem()
            {
                ProductId = _product2.Id,
                Quantity = 1,
                Discount = 0,
                Price = _product2.Price,
                Tax = 0,
                TaxPercent = 0,
                FinalPrice = _product2.Price
            });

            _cartService.SetDiscountCoupon(_registeredUser.Id, "validcoupon");

            var cart = _cartService.GetCart(_registeredUser.Id);
            _priceAccountant.RefreshCartParameters(cart);
            var expected = 12.5m;
            Assert.AreEqual(expected, cart.Discount); //discount coupon is percentage
        }

        [Test]
        public void Manual_Discount_Product_Succeeds()
        {
            _autoCoupon.Enabled = false;
            _discountCouponService.Update(_autoCoupon);

            _validCoupon.RestrictionType = RestrictionType.Products;
            _discountCouponService.Update(_validCoupon);

            _discountCouponService.SetRestrictionIdentifiers(_validCoupon.Id, new List<string>() { _product2.Id.ToString() });

            //clear cart
            _cartService.ClearCart(_registeredUser.Id);

            //add items
            _cartService.AddToCart(_registeredUser.Id, new CartItem()
            {
                ProductId = _product1.Id,
                Quantity = 1,
                Discount = 0,
                Price = _product1.Price,
                Tax = 0,
                TaxPercent = 0,
                FinalPrice = _product1.Price
            });
            _cartService.AddToCart(_registeredUser.Id, new CartItem()
            {
                ProductId = _product2.Id,
                Quantity = 1,
                Discount = 0,
                Price = _product2.Price,
                Tax = 0,
                TaxPercent = 0,
                FinalPrice = _product2.Price
            });

            _cartService.SetDiscountCoupon(_registeredUser.Id, "validcoupon");

            var cart = _cartService.GetCart(_registeredUser.Id);
            _priceAccountant.RefreshCartParameters(cart);
            
            Assert.AreEqual(0, cart.Discount); //discount coupon is percentage
            Assert.AreEqual(0, cart.CartItems[0].Discount);
            Assert.AreEqual(8.5m, cart.CartItems[1].Discount); //discount coupon is percentage
        }

        [Test]
        public void Manual_Discount_Categories_Succeeds()
        {
            _autoCoupon.Enabled = false;
            _discountCouponService.Update(_autoCoupon);

            _validCoupon.RestrictionType = RestrictionType.Categories;
            _discountCouponService.Update(_validCoupon);

            _discountCouponService.SetRestrictionIdentifiers(_validCoupon.Id, new List<string>() { _category.Id.ToString() });

            //clear cart
            _cartService.ClearCart(_registeredUser.Id);

            //add items
            _cartService.AddToCart(_registeredUser.Id, new CartItem()
            {
                ProductId = _product1.Id,
                Quantity = 1,
                Discount = 0,
                Price = _product1.Price,
                Tax = 0,
                TaxPercent = 0,
                FinalPrice = _product1.Price
            });
            _cartService.AddToCart(_registeredUser.Id, new CartItem()
            {
                ProductId = _product2.Id,
                Quantity = 1,
                Discount = 0,
                Price = _product2.Price,
                Tax = 0,
                TaxPercent = 0,
                FinalPrice = _product2.Price
            });

            _cartService.SetDiscountCoupon(_registeredUser.Id, "validcoupon");

            var cart = _cartService.GetCart(_registeredUser.Id);
            _priceAccountant.RefreshCartParameters(cart);

            Assert.AreEqual(0, cart.Discount); //discount coupon is percentage
            Assert.AreEqual(4, cart.CartItems[0].Discount);
            Assert.AreEqual(0, cart.CartItems[1].Discount); //discount coupon is percentage
        }

        [Test]
        public void Manual_Discount_Role_Succeeds()
        {
            _autoCoupon.Enabled = false;
            _discountCouponService.Update(_autoCoupon);

            _validCoupon.RestrictionType = RestrictionType.Roles;
            _discountCouponService.Update(_validCoupon);

            _discountCouponService.SetRestrictionIdentifiers(_validCoupon.Id, new List<string>() { _registeredRole.Id.ToString() });

            //clear cart
            _cartService.ClearCart(_registeredUser.Id);

            //add items
            _cartService.AddToCart(_registeredUser.Id, new CartItem()
            {
                ProductId = _product1.Id,
                Quantity = 1,
                Discount = 0,
                Price = _product1.Price,
                Tax = 0,
                TaxPercent = 0,
                FinalPrice = _product1.Price
            });
            _cartService.AddToCart(_registeredUser.Id, new CartItem()
            {
                ProductId = _product2.Id,
                Quantity = 1,
                Discount = 0,
                Price = _product2.Price,
                Tax = 0,
                TaxPercent = 0,
                FinalPrice = _product2.Price
            });

            var result = _cartService.SetDiscountCoupon(_registeredUser.Id, "validcoupon");

            var cart = _cartService.GetCart(_registeredUser.Id);
            _priceAccountant.RefreshCartParameters(cart);

            Assert.AreEqual(DiscountApplicationStatus.Success, result);
            Assert.AreEqual(0, cart.Discount); //discount coupon is percentage
            Assert.AreEqual(4, cart.CartItems[0].Discount);
            Assert.AreEqual(8.5, cart.CartItems[1].Discount); //discount coupon is percentage

            //for visitor
            //clear cart
            _cartService.ClearCart(_visitor.Id);

            //add items
            _cartService.AddToCart(_visitor.Id, new CartItem()
            {
                ProductId = _product1.Id,
                Quantity = 1,
                Discount = 0,
                Price = _product1.Price,
                Tax = 0,
                TaxPercent = 0,
                FinalPrice = _product1.Price
            });
            _cartService.AddToCart(_visitor.Id, new CartItem()
            {
                ProductId = _product2.Id,
                Quantity = 1,
                Discount = 0,
                Price = _product2.Price,
                Tax = 0,
                TaxPercent = 0,
                FinalPrice = _product2.Price
            });

            result = _cartService.SetDiscountCoupon(_visitor.Id, "validcoupon");

            cart = _cartService.GetCart(_visitor.Id);
            _priceAccountant.RefreshCartParameters(cart);

            Assert.AreEqual(DiscountApplicationStatus.NotEligibleForCart, result);
            Assert.AreEqual(0, cart.Discount); //discount coupon is percentage
            Assert.AreEqual(0, cart.CartItems[0].Discount);
            Assert.AreEqual(0, cart.CartItems[1].Discount); //discount coupon is percentage
        }

        [Test]
        public void Manual_Discount_User_Succeeds()
        {
            _autoCoupon.Enabled = false;
            _discountCouponService.Update(_autoCoupon);

            _validCoupon.RestrictionType = RestrictionType.Users;
            _discountCouponService.Update(_validCoupon);

            _discountCouponService.SetRestrictionIdentifiers(_validCoupon.Id, new List<string>() { _registeredUser.Id.ToString() });

            //clear cart
            _cartService.ClearCart(_registeredUser.Id);

            //add items
            _cartService.AddToCart(_registeredUser.Id, new CartItem()
            {
                ProductId = _product1.Id,
                Quantity = 1,
                Discount = 0,
                Price = _product1.Price,
                Tax = 0,
                TaxPercent = 0,
                FinalPrice = _product1.Price
            });
            _cartService.AddToCart(_registeredUser.Id, new CartItem()
            {
                ProductId = _product2.Id,
                Quantity = 1,
                Discount = 0,
                Price = _product2.Price,
                Tax = 0,
                TaxPercent = 0,
                FinalPrice = _product2.Price
            });

            var result = _cartService.SetDiscountCoupon(_registeredUser.Id, "validcoupon");

            var cart = _cartService.GetCart(_registeredUser.Id);
            _priceAccountant.RefreshCartParameters(cart);

            Assert.AreEqual(DiscountApplicationStatus.Success, result);
            Assert.AreEqual(0, cart.Discount); //discount coupon is percentage
            Assert.AreEqual(4, cart.CartItems[0].Discount);
            Assert.AreEqual(8.5, cart.CartItems[1].Discount);

            //clear cart
            _cartService.ClearCart(_visitor.Id);

            //add items
            _cartService.AddToCart(_visitor.Id, new CartItem()
            {
                ProductId = _product1.Id,
                Quantity = 1,
                Discount = 0,
                Price = _product1.Price,
                Tax = 0,
                TaxPercent = 0,
                FinalPrice = _product1.Price
            });
            _cartService.AddToCart(_visitor.Id, new CartItem()
            {
                ProductId = _product2.Id,
                Quantity = 1,
                Discount = 0,
                Price = _product2.Price,
                Tax = 0,
                TaxPercent = 0,
                FinalPrice = _product2.Price
            });

            result = _cartService.SetDiscountCoupon(_visitor.Id, "validcoupon");
            

            cart = _cartService.GetCart(_visitor.Id);
            _priceAccountant.RefreshCartParameters(cart);
            Assert.AreEqual(DiscountApplicationStatus.NotEligibleForCart, result);
            Assert.AreEqual(0, cart.Discount); //discount coupon is percentage
            Assert.AreEqual(0, cart.CartItems[0].Discount);
            Assert.AreEqual(0, cart.CartItems[1].Discount);
        }

        [Test]
        public void Manual_Discount_Manufacturer_Succeeds()
        {

            _autoCoupon.Enabled = false;
            _discountCouponService.Update(_autoCoupon);

            _validCoupon.RestrictionType = RestrictionType.Manufacturers;
            _discountCouponService.Update(_validCoupon);

            _discountCouponService.SetRestrictionIdentifiers(_validCoupon.Id, new List<string>() { _manufacturer.Id.ToString() });

            //clear registered user cart
            _cartService.ClearCart(_registeredUser.Id);
            //add items
            _cartService.AddToCart(_registeredUser.Id, new CartItem()
            {
                ProductId = _product1.Id,
                Quantity = 2,
                Discount = 0,
                Price = _product1.Price,
                Tax = 0,
                TaxPercent = 0,
                FinalPrice = _product1.Price
            });

            _cartService.AddToCart(_registeredUser.Id, new CartItem()
            {
                ProductId = _product2.Id,
                Quantity = 2,
                Discount = 0,
                Price = _product2.Price,
                Tax = 0,
                TaxPercent = 0,
                FinalPrice = _product2.Price
            });

            var result = _cartService.SetDiscountCoupon(_registeredUser.Id, "validcoupon");

            var rCart = _cartService.GetCart(_registeredUser.Id);
            _priceAccountant.RefreshCartParameters(rCart);

            Assert.AreEqual(DiscountApplicationStatus.Success, result);
            Assert.AreEqual(0, rCart.Discount);
            Assert.AreEqual(8, rCart.CartItems[0].Discount);
            Assert.AreEqual(0, rCart.CartItems[1].Discount);
        }

        [Test]
        public void Manual_Discount_Vendor_Succeeds()
        {
            _autoCoupon.Enabled = false;
            _discountCouponService.Update(_autoCoupon);

            _validCoupon.RestrictionType = RestrictionType.Vendors;
            _discountCouponService.Update(_validCoupon);

            _discountCouponService.SetRestrictionIdentifiers(_validCoupon.Id, new List<string>() { _vendor.Id.ToString() });

            //clear registered user cart
            _cartService.ClearCart(_registeredUser.Id);
            //add items
            _cartService.AddToCart(_registeredUser.Id, new CartItem()
            {
                ProductId = _product1.Id,
                Quantity = 2,
                Discount = 0,
                Price = _product1.Price,
                Tax = 0,
                TaxPercent = 0,
                FinalPrice = _product1.Price
            });

            _cartService.AddToCart(_registeredUser.Id, new CartItem()
            {
                ProductId = _product2.Id,
                Quantity = 2,
                Discount = 0,
                Price = _product2.Price,
                Tax = 0,
                TaxPercent = 0,
                FinalPrice = _product2.Price
            });
            var result = _cartService.SetDiscountCoupon(_registeredUser.Id, "validcoupon");

            var rCart = _cartService.GetCart(_registeredUser.Id);
            _priceAccountant.RefreshCartParameters(rCart);
            Assert.AreEqual(DiscountApplicationStatus.Success, result);
            Assert.AreEqual(0, rCart.Discount);
            Assert.AreEqual(0, rCart.CartItems[0].Discount);
            Assert.AreEqual(17, rCart.CartItems[1].Discount);
        }
    }

    [TestFixture]
    public class SqlServerPriceAccountantTests : PriceAccountantTests
    {
        public SqlServerPriceAccountantTests()
        {
            TestDbInit.SqlServer(MsSqlConnectionString);
        }
    }

    [TestFixture]
    public class MySqlPriceAccountantTests : PriceAccountantTests
    {
        public MySqlPriceAccountantTests()
        {
            TestDbInit.MySql(MySqlConnectionString);
        }
    }
}