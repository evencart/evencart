using EvenCart.Areas.Administration.Models.Shop;
using EvenCart.Data.Entity.Purchases;
using EvenCart.Data.Entity.Shop;
using EvenCart.Infrastructure.Mvc.ModelFactories;

namespace EvenCart.Areas.Administration.Factories.Products
{
    public interface IDownloadModelFactory : IModelFactory<Download, DownloadModel>
    {
        OrderDownloadModel Create(Download download, ItemDownload itemDownload);
    }
}