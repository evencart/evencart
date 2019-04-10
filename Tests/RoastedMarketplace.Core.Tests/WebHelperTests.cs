using NUnit.Framework;
using NUnit.Framework.Internal;

namespace RoastedMarketplace.Core.Tests
{
    [TestFixture()]
    public class WebHelperTests
    {
        [Test]
        public void Get_Url_From_Path_Succeeds()
        {
            Assert.AreEqual("/test-path", WebHelper.GetUrlFromPath("/test-path"));
            Assert.AreEqual("http://teststore.com/test-path", WebHelper.GetUrlFromPath("/test-path", "teststore.com"));
            Assert.AreEqual("https://teststore.com/test-path", WebHelper.GetUrlFromPath("/test-path", "teststore.com", "https"));
            Assert.AreEqual("/test-path", WebHelper.GetUrlFromPath("~/test-path"));
        }
    }
}