using System.Linq;
using EvenCart.Data.Entity.Shop;
using EvenCart.Infrastructure;
using EvenCart.Infrastructure.MediaServices;
using EvenCart.Infrastructure.Routing;
using Ui.SearchPlus.Data;

namespace Ui.SearchPlus.Factories
{
    public class SearchTermModelFactory : ISearchTermModelFactory
    {
        private readonly IMediaAccountant _mediaAccountant;

        public SearchTermModelFactory(IMediaAccountant mediaAccountant)
        {
            _mediaAccountant = mediaAccountant;
        }

        public AutoCompleteResultModel Create(Product product)
        {
            return new AutoCompleteResultModel()
            {
                Name = product.Name,
                Url = ApplicationEngine.RouteUrl(RouteNames.SingleProduct,
                    new {SeName = product.SeoMeta.Slug, Id = product.Id}),
                ThumbnailUrl = _mediaAccountant.GetPictureUrl(product.MediaItems?.FirstOrDefault(),
                    returnDefaultIfNotFound: true)
            };
        }

        public AutoCompleteResultModel Create(SearchTerm searchTerm)
        {
            return new AutoCompleteResultModel()
            {
                Name = searchTerm.Term,
                Url = ApplicationEngine.RouteUrl(RouteNames.ProductsSearchPage,
                    new { search = searchTerm.Term })
            };
        }
    }
}