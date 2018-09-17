using System;
using NUnit.Framework;
using RoastedMarketplace.Data.Entity.Addresses;
using RoastedMarketplace.Data.Entity.Settings;
using RoastedMarketplace.Data.Entity.Shop;
using RoastedMarketplace.Data.Entity.Taxes;
using RoastedMarketplace.Data.Entity.Users;
using RoastedMarketplace.Services.Addresses;
using RoastedMarketplace.Services.Products;
using RoastedMarketplace.Services.Settings;
using RoastedMarketplace.Services.Taxes;
using RoastedMarketplace.Services.Users;

namespace RoastedMarketplace.Services.Tests.Products
{
    public abstract class TaxServiceTests : BaseTest
    {
        private IProductService _productService;
        private ITaxService _taxService;
        private ITaxAccountant _taxAccountant;
        private ITaxRateService _taxRateService;
        private ICountryService _countryService;
        private IStateOrProvinceService _stateOrProvinceService;
        private IUserService _userService;
        private IRoleService _roleService;
        private IAddressService _addressService;
        private TaxSettings _taxSettings;
        private ISettingService _settingService;
       
        private Country[] _countries;
        private StateOrProvince[] _states;
        private Tax[] _taxes;
        private TaxRate[] _taxRates;
        private Product[] _products;
        private Address[] _addresses;
        private User[] _users;

        [OneTimeSetUp]
        public void SeedData()
        {
            _settingService = Resolve<ISettingService>();
            _roleService = Resolve<IRoleService>();
            _addressService = Resolve<IAddressService>();
            _userService = Resolve<IUserService>();
            _taxRateService = Resolve<ITaxRateService>();
            _taxService = Resolve<ITaxService>();
            _taxAccountant = Resolve<ITaxAccountant>();
            _productService = Resolve<IProductService>();
            _countryService = Resolve<ICountryService>();
            _stateOrProvinceService = Resolve<IStateOrProvinceService>();
            _taxSettings = Resolve<TaxSettings>();

            _countries = new[]
            {
                new Country() { Name = "India"},
                new Country() { Name = "USA"},
            };
            _countryService.Insert(_countries);

            _states = new[]
            {
                new StateOrProvince() {Name = "Madhya Pradesh", CountryId = _countries[0].Id},
                new StateOrProvince() {Name = "Maharashtra", CountryId = _countries[0].Id},
                new StateOrProvince() {Name = "Gujarat", CountryId = _countries[0].Id},

                new StateOrProvince() {Name = "California", CountryId = _countries[1].Id},
                new StateOrProvince() {Name = "Texas", CountryId = _countries[1].Id},
                new StateOrProvince() {Name = "Washington", CountryId = _countries[1].Id},
            };
            _stateOrProvinceService.Insert(_states);

            _taxes = new[]
            {
                new Tax() { Name = "Cloths"},
                new Tax() { Name = "Books"},
                new Tax() { Name = "Software"},
            };
            _taxService.Insert(_taxes);

            _taxRates = new[]
            {
                new TaxRate() { TaxId = _taxes[0].Id, CountryId = _countries[0].Id, Rate = 12},
                new TaxRate() { TaxId = _taxes[0].Id, CountryId = _countries[0].Id, StateOrProvinceId = _states[0].Id, Rate = 15},
                new TaxRate() { TaxId = _taxes[0].Id, CountryId = _countries[0].Id, StateOrProvinceId = _states[0].Id, ZipOrPostalCode = "452001", Rate = 18},
                new TaxRate() { TaxId = _taxes[1].Id, CountryId = _countries[0].Id, StateOrProvinceId = _states[0].Id, Rate = 0},
                new TaxRate() { TaxId = _taxes[2].Id, CountryId = _countries[1].Id, Rate = 20},
            };
            _taxRateService.Insert(_taxRates);

            _taxSettings.DefaultTaxRate = 1;
            _settingService.Save(_taxSettings);

            var user = new User()
            {
                Email = "taxpayer@ecomm.com",
                Active = true,
                DateCreated = DateTime.Today,
                DateUpdated = DateTime.UtcNow
            };
            _userService.Insert(user);
            _roleService.AssignRoleToUser(SystemRoleNames.Registered, user);
            _users = new User[] { user };
            //add address
            var address1 = new Address()
            {
                Address1 = "123, Some Random Street",
                Address2 = "Some random road",
                City = "Some Random City",
                CountryId = _countries[0].Id,
                StateProvinceId = _states[0].Id,
                ZipPostalCode = "452000",
                UserId = user.Id
            };
            _addressService.Insert(address1);

            var address2 = new Address() {
                Address1 = "456, Some Random Street",
                Address2 = "Some random road",
                City = "Some Random City",
                CountryId = _countries[0].Id,
                StateProvinceId = _states[0].Id,
                ZipPostalCode = "452001",
                UserId = user.Id
            };
            _addressService.Insert(address2);

            var address3 = new Address() {
                Address1 = "456, Some Random Street",
                Address2 = "Some random road",
                City = "Some Random City",
                CountryId = _countries[1].Id,
                StateProvinceId = _states[0].Id,
                ZipPostalCode = "452001",
                UserId = user.Id
            };
            _addressService.Insert(address3);
            _addresses = new Address[] {address1, address2, address3};
            var product1 = new Product() {
                Name = "Clothes",
                CanOrderWhenOutOfStock = true,
                ChargeTaxes = true,
                ComparePrice = 100,
                Price = 80,
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow,
                TaxId = _taxes[0].Id
            };
            var product2 = new Product() {
                Name = "Microsoft Surface Pro",
                CanOrderWhenOutOfStock = true,
                ChargeTaxes = false,
                ComparePrice = 1000,
                Price = 900,
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow,
            };
            var product3 = new Product() {
                Name = "Learn Microsoft Surface Pro in 30 days",
                CanOrderWhenOutOfStock = true,
                ChargeTaxes = true,
                TaxId = _taxes[1].Id,
                ComparePrice = 100,
                Price = 100,
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow,
            };
            _products = new[] {product1, product2, product3};
            _productService.Insert(_products);
        }

        [Test]
        public void Tax_Rate_Retrieval_Succeeds()
        {
            var rate = _taxAccountant.GetFinalTaxRate(_products[0], _addresses[0]);
            Assert.AreEqual(_taxRates[1].Rate, rate); //15

            rate = _taxAccountant.GetFinalTaxRate(_products[0], _addresses[1]);
            Assert.AreEqual(_taxRates[2].Rate, rate); //18

            rate = _taxAccountant.GetFinalTaxRate(_products[0], _addresses[2]);
            Assert.AreEqual(_taxSettings.DefaultTaxRate, rate); //default tax rate

            rate = _taxAccountant.GetFinalTaxRate(_products[1], _addresses[0]);
            Assert.AreEqual(0, rate); //no tax is charged

            rate = _taxAccountant.GetFinalTaxRate(_products[2], _addresses[0]);
            Assert.AreEqual(_taxRates[3].Rate, rate); // 0

            rate = _taxAccountant.GetFinalTaxRate(_products[2], _addresses[1]);
            Assert.AreEqual(_taxRates[3].Rate, rate); //0

            rate = _taxAccountant.GetFinalTaxRate(_products[2], _addresses[2]);
            Assert.AreEqual(_taxSettings.DefaultTaxRate, rate); //default
        }
    }

    [TestFixture]
    public class SqlServerTaxServiceTests : TaxServiceTests
    {
        public SqlServerTaxServiceTests()
        {
            TestDbInit.SqlServer(MsSqlConnectionString);
        }
    }
}