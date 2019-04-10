using NUnit.Framework;
using RoastedMarketplace.Data.Entity.Cultures;
using RoastedMarketplace.Services.Formatter;

namespace RoastedMarketplace.Services.Tests.Formatters
{
    [TestFixture]
    public class RoundingServiceTests : BaseTest
    {
        private readonly IRoundingService _roundingService;
        public RoundingServiceTests()
        {
            _roundingService = Resolve<IRoundingService>();
        }

        [Test]
        public void Rounding_Tests_Succeeded()
        {
            Assert.AreEqual(2.55m, _roundingService.Round(2.55m, 2, Rounding.Default));
            Assert.AreEqual(2.56m, _roundingService.Round(2.557m, 2, Rounding.Default));
            Assert.AreEqual(2.55m, _roundingService.Round(2.554m, 2, Rounding.Default));

            Assert.AreEqual(3.00m, _roundingService.Round(2.73m, 2, Rounding.RoundDot00));
            Assert.AreEqual(1.00m, _roundingService.Round(1.10m, 2, Rounding.RoundDot00));
            Assert.AreEqual(1.00m, _roundingService.Round(1.159m, 3, Rounding.RoundDot00));

            Assert.AreEqual(1.00m, _roundingService.Round(1.09m, 2, Rounding.RoundDot50Or00));
            Assert.AreEqual(1.50m, _roundingService.Round(1.29m, 2, Rounding.RoundDot50Or00));
            Assert.AreEqual(1.50m, _roundingService.Round(1.49m, 2, Rounding.RoundDot50Or00));
            Assert.AreEqual(1.50m, _roundingService.Round(1.50m, 2, Rounding.RoundDot50Or00));


            Assert.AreEqual(0.99m, _roundingService.Round(1.09m, 2, Rounding.RoundDot99));
            Assert.AreEqual(0.99m, _roundingService.Round(1.29m, 2, Rounding.RoundDot99));
            Assert.AreEqual(0.99m, _roundingService.Round(1.49m, 2, Rounding.RoundDot99));
            Assert.AreEqual(1.99m, _roundingService.Round(1.59m, 2, Rounding.RoundDot99));

            Assert.AreEqual(0.99m, _roundingService.Round(1.09m, 2, Rounding.RoundDot99Or49));
            Assert.AreEqual(1.49m, _roundingService.Round(1.29m, 2, Rounding.RoundDot99Or49));
            Assert.AreEqual(1.99m, _roundingService.Round(1.79m, 2, Rounding.RoundDot99Or49));
            Assert.AreEqual(1.49m, _roundingService.Round(1.59m, 2, Rounding.RoundDot99Or49));
            Assert.AreEqual(1.49m, _roundingService.Round(1.49m, 2, Rounding.RoundDot99Or49));
            Assert.AreEqual(1.99m, _roundingService.Round(1.99m, 2, Rounding.RoundDot99Or49));
            Assert.AreEqual(1.99m, _roundingService.Round(1.75m, 2, Rounding.RoundDot99Or49));

            Assert.AreEqual(1.10m, _roundingService.Round(1.09m, 2, Rounding.RoundDotX5));
            Assert.AreEqual(1.30m, _roundingService.Round(1.29m, 2, Rounding.RoundDotX5));
            Assert.AreEqual(1.80m, _roundingService.Round(1.79m, 2, Rounding.RoundDotX5));
            Assert.AreEqual(1.60m, _roundingService.Round(1.59m, 2, Rounding.RoundDotX5));
            Assert.AreEqual(1.45m, _roundingService.Round(1.45m, 2, Rounding.RoundDotX5));
            Assert.AreEqual(2.00m, _roundingService.Round(1.99m, 2, Rounding.RoundDotX5));
            Assert.AreEqual(1.95m, _roundingService.Round(1.94m, 2, Rounding.RoundDotX5));
            Assert.AreEqual(1.95m, _roundingService.Round(1.96m, 2, Rounding.RoundDotX5));

            Assert.AreEqual(1.10m, _roundingService.Round(1.09m, 2, Rounding.RoundDotX0));
            Assert.AreEqual(1.30m, _roundingService.Round(1.29m, 2, Rounding.RoundDotX0));
            Assert.AreEqual(1.80m, _roundingService.Round(1.79m, 2, Rounding.RoundDotX0));
            Assert.AreEqual(1.60m, _roundingService.Round(1.59m, 2, Rounding.RoundDotX0));
            Assert.AreEqual(1.50m, _roundingService.Round(1.45m, 2, Rounding.RoundDotX0));
            Assert.AreEqual(1.40m, _roundingService.Round(1.44m, 2, Rounding.RoundDotX0));
            Assert.AreEqual(2.00m, _roundingService.Round(1.99m, 2, Rounding.RoundDotX0));
            Assert.AreEqual(1.90m, _roundingService.Round(1.94m, 2, Rounding.RoundDotX0));
            Assert.AreEqual(2.00m, _roundingService.Round(1.96m, 2, Rounding.RoundDotX0));
        }
    }

}