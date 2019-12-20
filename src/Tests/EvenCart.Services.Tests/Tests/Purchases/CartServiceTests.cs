using System;
using EvenCart.Data.Entity.Purchases;
using EvenCart.Data.Entity.Users;
using EvenCart.Services.Products;
using EvenCart.Services.Purchases;
using EvenCart.Services.Users;
using NUnit.Framework;

namespace EvenCart.Services.Tests.Purchases
{
    public abstract class CartServiceTests : BaseTest
    {
        private ICartService _cartService;
        private IUserService _userService;
        private IProductService _productService;

        private User _testUser;

        [SetUp]
        protected void Setup()
        {
            _cartService = Resolve<ICartService>();
            _userService = Resolve<IUserService>();
            _productService = Resolve<IProductService>();

            _testUser = new User()
            {
                Email = "cartservicetests@teststore.com",
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow,
            };
            _userService.Insert(_testUser);
        }

        [Test]
        public void New_Cart_Creation_Succeeds()
        {
            var cart = _cartService.GetCart(_testUser.Id);
            Assert.IsNotNull(cart);
            Assert.Greater(cart.Id, 0);
        }

        [Test]
        public void Cart_Item_Addition_And_Removal_Succeeds()
        {
            var products = EvenCart.Tests.Data.Products.GetList();
            _productService.Insert(products[0]);
            _productService.Insert(products[1]);

            //add items
            _cartService.AddToCart(_testUser.Id, new CartItem()
            {
                ProductId = products[0].Id,
                Quantity = 1,
                Discount = 0,
                Price = products[0].Price,
                Tax = 0,
                TaxPercent = 0,
                FinalPrice = products[0].Price
            });
            _cartService.AddToCart(_testUser.Id, new CartItem()
            {
                ProductId = products[1].Id,
                Quantity = 4,
                Discount = 0,
                Price = products[1].Price,
                Tax = 0,
                TaxPercent = 0,
                FinalPrice = products[1].Price * 4
            });

            //did addition work
            var cart = _cartService.GetCart(_testUser.Id);
            Assert.AreEqual(2, cart.CartItems.Count);

            //check for remove now
            _cartService.RemoveFromCart(cart.CartItems[0].Id);
            cart = _cartService.GetCart(_testUser.Id);
            Assert.AreEqual(1, cart.CartItems.Count);

            //and clear
            _cartService.ClearCart(_testUser.Id);
            cart = _cartService.GetCart(_testUser.Id);
            Assert.AreEqual(0, cart.CartItems.Count);
        }
    }
}