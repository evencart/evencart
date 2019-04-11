using System.Collections.Generic;
using EvenCart.Core.Services;
using EvenCart.Data.Entity.Pages;

namespace EvenCart.Services.Pages
{
    public interface IContentPageService : IFoundationEntityService<ContentPage>
    {
        IList<ContentPage> GetContentPages(out int totalResults, string search = null, int page = 1, int count = int.MaxValue);
    }
}