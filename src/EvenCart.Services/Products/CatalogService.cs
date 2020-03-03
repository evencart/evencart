using System.Linq;
using EvenCart.Core.Services;
using EvenCart.Core.Services.Events;
using EvenCart.Data.Entity.Shop;

namespace EvenCart.Services.Products
{
    public class CatalogService : FoundationEntityService<Catalog>, ICatalogService
    {
        private readonly IEventPublisherService _eventPublisherService;

        public CatalogService(IEventPublisherService eventPublisherService)
        {
            _eventPublisherService = eventPublisherService;
        }

        public override Catalog Get(int id)
        {
            var query = _eventPublisherService.Filter(Repository.Where(x => x.Id == id));
            return query.SelectNested()
                .FirstOrDefault();
        }
    }
}