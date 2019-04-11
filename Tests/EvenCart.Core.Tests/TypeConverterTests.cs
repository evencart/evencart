using System;
using EvenCart.Core.Infrastructure.Utils;
using EvenCart.Data.Extensions;
using NUnit.Framework;

namespace EvenCart.Core.Tests
{
    [TestFixture]
    public class TypeConverterTests
    {
        [Test]
        public void Type_Conversion_Succeeds()
        {
            Assert.AreEqual(1, TypeConverter.CastValue(typeof(int), "1"));
            Assert.AreEqual(1m, TypeConverter.CastValue(typeof(decimal), "1"));
            Assert.AreEqual(1m, TypeConverter.CastValue(typeof(double), "1"));
            Assert.AreEqual(null, TypeConverter.CastValue(typeof(int?), null));
            Assert.AreEqual(1, TypeConverter.CastValue(typeof(int?), 1));
            Assert.AreEqual(2, TypeConverter.CastValue(typeof(int), 1.5m));

            Assert.AreEqual(TestEnum.ValueOne, TypeConverter.CastValue(typeof(TestEnum), "0"));
            Assert.AreEqual(TestEnum.ValueTwo, TypeConverter.CastValue(typeof(TestEnum), "1"));
            Assert.AreEqual(TestEnum.ValueOne, TypeConverter.CastValue(typeof(TestEnum), "ValueOne"));
            Assert.AreEqual(TestEnum.ValueTwo, TypeConverter.CastValue(typeof(TestEnum), "ValueTwo"));

            Assert.AreEqual(true, TypeConverter.CastValue(typeof(bool), "true"));
            Assert.AreEqual(true, TypeConverter.CastValue(typeof(bool), "True"));
            Assert.AreEqual(true, TypeConverter.CastValue(typeof(bool), "on"));
            Assert.AreEqual(false, TypeConverter.CastValue(typeof(bool), "false"));
            Assert.AreEqual(false, TypeConverter.CastValue(typeof(bool), "False"));
            Assert.AreEqual(true, TypeConverter.CastValue(typeof(bool), "checked"));

            Assert.AreEqual(new Uri("http://www.teststore.com"), TypeConverter.CastValue(typeof(Uri), "http://www.teststore.com"));

            Assert.IsTrue("1".IsNumeric());
            Assert.IsTrue("1.5".IsNumeric());
            Assert.IsFalse("abc".IsNumeric());
            Assert.IsFalse("1".IsDateTime());
            Assert.IsTrue("13/12/2012".IsDateTime());
            Assert.IsFalse("12/13/2012".IsDateTime());
            Assert.IsTrue("sample@testdomain.com".IsValidEmail());
            Assert.IsFalse("@testdomain.com".IsValidEmail());
            Assert.IsFalse("sample@.com".IsValidEmail());
            Assert.IsFalse("1.5".IsInteger());

            Assert.IsTrue("red".IsColor());
            Assert.IsTrue("5A5A5A".IsColor());
            Assert.IsTrue("555".IsColor());
            Assert.IsFalse("DADAZA".IsColor());
            Assert.IsFalse("random".IsColor());
        }

        public enum TestEnum
        {
            ValueOne,
            ValueTwo
        }
    }
}