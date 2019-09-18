using System.Collections.Generic;

namespace EvenCart.Services.Seo
{
    public interface ISitemapProvider
    {
        IList<string> GetUrls();
    }
}