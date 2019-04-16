using System;
using EvenCart.Data.Extensions;
using EvenCart.Data.Helpers;
using NUnit.Framework;

namespace EvenCart.Common.Tests
{
    [TestFixture()]
    public class StringTests
    {
        [Test]
        public void String_Extensions_Tests_Succeed()
        {
            Assert.AreEqual("this is to test", "this  is  to test  ".CleanUpSpaces());
            
            Assert.AreEqual("This Is To Test", "this is to test".ToTitleCase());
            Assert.AreEqual("aVariableThatSays", "AVariableThatSays".ToCamelCase());
            Assert.AreEqual(true, "".IsNullEmptyOrWhiteSpace());
            Assert.AreEqual(true, " ".IsNullEmptyOrWhiteSpace());
            string s = null;
            Assert.AreEqual(true, s.IsNullEmptyOrWhiteSpace());

            Assert.AreEqual("Everything is great", "taerg si gnihtyrevE".Reverse());
            Assert.AreEqual("Red is a variant of Navy Blue", "Blue is a variant of Navy Blue".ReplaceFirstOccurance("Blue", "Red"));
            Assert.AreEqual("Red is a variant of Navy Blue", "blue is a variant of Navy Blue".ReplaceFirstOccurance("Blue", "Red", StringComparison.OrdinalIgnoreCase));

            Assert.AreEqual("lorem ipsum is sample text ", HtmlUtility.StripHtml("<h1>lorem</h1> <b>ipsum</b> is <a href=''>sample text</a> "));


            Assert.AreEqual("lorem ipsum is <a href=''>sample text</a> ", HtmlUtility.RemoveUnwantedTags("<h1>lorem</h1> <b>ipsum</b> is <a href=''>sample text</a> "));
            Assert.AreEqual("<h1>lorem</h1> <b>ipsum</b> is sample text ", HtmlUtility.RemoveUnwantedTags("<h1>lorem</h1> <b>ipsum</b> is <a href=''>sample text</a> ", new []{ "h1", "b"}));
        }
    }
}