using System;
using Genesis.Extensions;
using NUnit.Framework;

namespace EvenCart.Common.Tests
{
    [TestFixture]
    public class DateTimeExtensionTests
    {
        [Test]
        public void GetWeekRange_Succeeds()
        {
            new DateTime(2019, 4, 11).GetWeekRangeDates(out var startDate, out var endDate);
            Assert.AreEqual(new DateTime(2019, 4, 8), startDate);
            Assert.AreEqual(new DateTime(2019, 4, 14), endDate);

            new DateTime(2019, 2, 14).GetWeekRangeDates(out startDate, out endDate);
            Assert.AreEqual(new DateTime(2019, 2, 11), startDate);
            Assert.AreEqual(new DateTime(2019, 2, 17), endDate);

            new DateTime(2019, 2, 3).GetWeekRangeDates(out startDate, out endDate);
            Assert.AreEqual(new DateTime(2019, 1, 28), startDate);
            Assert.AreEqual(new DateTime(2019, 2, 3), endDate);
        }
    }
}