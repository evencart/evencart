using System;
using System.Linq;
using EvenCart.Data.Entity.Promotions;
using EvenCart.Services.Promotions;
using NUnit.Framework;

namespace EvenCart.Services.Tests.Promotions
{
    public abstract class DiscountServiceTests : BaseTest
    {
        private IDiscountCouponService _discountCouponService;

        [SetUp]
        public void Setup()
        {
            _discountCouponService = Resolve<IDiscountCouponService>();
        }

        [Test]
        public void Discount_Coupon_Persistance_Succeeds()
        {
            var discounts = new[]
            {
                new DiscountCoupon()
                {
                    Name = "Test Coupon One",
                    CalculationType = CalculationType.FixedAmount,
                    CouponCode = "TESTCOUPON",
                    HasCouponCode = true,
                    DiscountValue = 50,
                    Enabled = true,
                    StartDate = DateTime.UtcNow,
                    EndDate = DateTime.UtcNow.AddDays(5),
                    NumberOfTimesPerUser = 1,
                    TotalNumberOfTimes = 5
                },
                new DiscountCoupon()
                {
                    Name = "Test Coupon Two",
                    CalculationType = CalculationType.Percentage,
                    CouponCode = "TESTCOUPON",
                    HasCouponCode = true,
                    DiscountValue = 5,
                    Enabled = true,
                    StartDate = DateTime.UtcNow,
                    EndDate = DateTime.UtcNow.AddDays(5),
                    NumberOfTimesPerUser = 1,
                    TotalNumberOfTimes = 5,
                    MaximumDiscountAmount = 10
                },
                new DiscountCoupon()
                {
                    Name = "Expired Coupon",
                    CalculationType = CalculationType.Percentage,
                    HasCouponCode = false,
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


            //asserts
            Assert.AreEqual(discounts[1].Id, _discountCouponService.GetByCouponCode("testcoupon").Id);
            Assert.AreEqual(discounts[1].Id, _discountCouponService.Get(discounts[1].Id).Id);
            Assert.AreEqual(discounts[0].CouponCode, _discountCouponService.Get(discounts[0].Id).CouponCode);
            Assert.AreEqual(1, _discountCouponService.SearchDiscountCoupons("test", out var totalMatches, 1, 1).Count());
            Assert.AreEqual(2, totalMatches);
            //delete discounts
            _discountCouponService.Delete(x => x.Id > 0);
        }
    }

    [TestFixture]
    public class SqlServerDiscountServiceTests : DiscountServiceTests
    {
        public SqlServerDiscountServiceTests()
        {
            TestDbInit.SqlServer(MsSqlConnectionString);
        }
    }

    [TestFixture]
    public class MySqlDiscountServiceTests : DiscountServiceTests
    {
        public MySqlDiscountServiceTests()
        {
            TestDbInit.MySql(MySqlConnectionString);
        }
    }
}