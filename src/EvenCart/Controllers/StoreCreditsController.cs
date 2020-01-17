using System.Linq;
using DotEntity.Enumerations;
using EvenCart.Infrastructure.Mvc;
using EvenCart.Infrastructure.Routing;
using EvenCart.Models.Users;
using EvenCart.Services.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EvenCart.Controllers
{
    [Authorize]
    public class StoreCreditsController : FoundationController
    {
        private readonly IStoreCreditService _storeCreditService;
        public StoreCreditsController(IStoreCreditService storeCreditService)
        {
            _storeCreditService = storeCreditService;
        }

        [DualGet("~/account/store-credits", Name = RouteNames.AccountStoreCredits)]
        public IActionResult StoreCreditsInfo(int page = 1)
        {
            if (page < 1)
                page = 1;
            var count = 15;
            var storeCredits = _storeCreditService.Get(out var total, x => x.UserId == CurrentUser.Id, x => x.Id, RowOrder.Descending,
                page, count);
            var balance = _storeCreditService.GetBalance(CurrentUser.Id);
            var storeCreditModels = storeCredits.Select(x => new StoreCreditModel()
            {
                Description = x.Description,
                CreatedOn = x.CreatedOn,
                AvailableOn = x.AvailableOn,
                Credit = x.Credit,
                ExpiresOn = x.ExpiresOn
            }).ToList();
            return R.Success.WithGridResponse(total, page, count).With("storeCredits", storeCreditModels)
                .With("availableBalance", balance).Result;
        }
    }
}