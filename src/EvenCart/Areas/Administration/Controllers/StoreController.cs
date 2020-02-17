using EvenCart.Infrastructure.Mvc;
using EvenCart.Services.Products;

namespace EvenCart.Areas.Administration.Controllers
{
    public class StoreController : FoundationAdminController
    {
        private readonly IStoreService _storeService;
        public StoreController(IStoreService storeService)
        {
            _storeService = storeService;
        }


    }
}