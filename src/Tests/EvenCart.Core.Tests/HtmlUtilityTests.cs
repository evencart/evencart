using EvenCart.Data.Helpers;
using NUnit.Framework;

namespace EvenCart.Common.Tests
{
    [TestFixture]
    public class HtmlUtilityTests
    {
        [Test]
        public void HtmlUtility_Tests_Succeeed()
        {
            Assert.AreEqual("lorem ipsum is sample text ", HtmlUtility.StripHtml("<h1>lorem</h1> <b>ipsum</b> is <a href=''>sample text</a> "));


            Assert.AreEqual("lorem ipsum is <a href=''>sample text</a> ", HtmlUtility.RemoveUnwantedTags("<h1>lorem</h1> <b>ipsum</b> is <a href=''>sample text</a> "));
            Assert.AreEqual("<h1>lorem</h1> <b>ipsum</b> is sample text ", HtmlUtility.RemoveUnwantedTags("<h1>lorem</h1> <b>ipsum</b> is <a href=''>sample text</a> ", new[] { "h1", "b" }));
        }
    }
}