using System.Collections.Generic;
using RoastedMarketplace.Core.Services;
using RoastedMarketplace.Data.Entity.Pages;

namespace RoastedMarketplace.Services.Pages
{
    public interface IContentPageService : IFoundationEntityService<ContentPage>
    {
        IList<ContentPage> GetContentPages(out int totalResults, string search = null, int page = 1, int count = int.MaxValue);
    }
}