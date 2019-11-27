using EvenCart.Areas.Administration.Models.Shop;
using EvenCart.Data.Entity.Purchases;
using EvenCart.Data.Entity.Shop;
using EvenCart.Infrastructure;
using EvenCart.Infrastructure.Mvc.ModelFactories;
using EvenCart.Infrastructure.Routing;

namespace EvenCart.Areas.Administration.Factories.Products
{
    public class DownloadModelFactory : IDownloadModelFactory
    {
        private readonly IModelMapper _modelMapper;

        public DownloadModelFactory(IModelMapper modelMapper)
        {
            _modelMapper = modelMapper;
        }

        public DownloadModel Create(Download entity)
        {
            var model = _modelMapper.Map<DownloadModel>(entity);
            model.DownloadUrl = ApplicationEngine.RouteUrl(AdminRouteNames.AdminDownloadFile, new { id = entity.Id });
            return model;
        }

        public OrderDownloadModel Create(Download download, ItemDownload itemDownload)
        {
            var model = new OrderDownloadModel()
            {
                Title = download.Title,
                Description = download.Description,
                Active = itemDownload?.Active ?? download.DownloadActivationType != DownloadActivationType.Manual,
                DownloadCount = itemDownload?.DownloadCount ?? 0,
                DownloadUrl = ApplicationEngine.RouteUrl(AdminRouteNames.AdminDownloadFile, new { id = download.Id }),
                ItemDownloadId = itemDownload?.Id ?? 0,
                DownloadId = download.Id
            };
            return model;
        }
    }
}