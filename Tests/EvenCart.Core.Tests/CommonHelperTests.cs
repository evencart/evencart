using NUnit.Framework;

namespace EvenCart.Core.Tests
{
    [TestFixture]
    public class CommonHelperTests
    {
        [Test]
        public void Generate_Slug_Tests_Succeed()
        {
            Assert.AreEqual("sample-title", CommonHelper.GenerateSlug("Sample Title"));
            Assert.AreEqual("sample-title-some-more-text", CommonHelper.GenerateSlug("Sample Title : Some more text"));
            Assert.AreEqual("sample_title", CommonHelper.GenerateSlug("Sample_Title"));
            Assert.AreEqual("sample-title", CommonHelper.GenerateSlug("Samplé Titlé"));
            Assert.AreEqual("essential-brute", CommonHelper.GenerateSlug("èssential brûté"));
        }
    }
}