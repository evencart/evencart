using EvenCart.Data.Extensions;
using NUnit.Framework;

namespace EvenCart.Common.Tests
{
    [TestFixture()]
    public class NumericExtensionsTests
    {
        [Test]
        public void Numeric_Tests_Succeed()
        {
            Assert.AreEqual(260, NumericExtensions.CeilTen(255));
            Assert.AreEqual(250, NumericExtensions.FloorTen(255));
            Assert.AreEqual(300, NumericExtensions.CeilHundred(255));
            Assert.AreEqual(200, NumericExtensions.FloorHundred(255));
            Assert.AreEqual(260, NumericExtensions.RoundTen(255));
            Assert.AreEqual(300, NumericExtensions.RoundHundred(255));
            Assert.AreEqual(250, NumericExtensions.RoundTen(254));
            Assert.AreEqual(200, NumericExtensions.RoundHundred(244));
        }
    }
}