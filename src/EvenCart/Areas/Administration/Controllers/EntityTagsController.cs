using System.Linq;
using EvenCart.Infrastructure.Helpers;
using EvenCart.Infrastructure.Mvc;
using EvenCart.Infrastructure.Routing;
using EvenCart.Services.Common;
using Microsoft.AspNetCore.Mvc;

namespace EvenCart.Areas.Administration.Controllers
{
    public class EntityTagsController : FoundationAdminController
    {
        private readonly IEntityTagService _entityTagService;

        public EntityTagsController(IEntityTagService entityTagService)
        {
            _entityTagService = entityTagService;
        }

        [DualGet("suggestions", Name = AdminRouteNames.GetEntityTagsSuggestions, OnlyApi = true)]
        public IActionResult Suggestions(string q = null)
        {
            var tags = _entityTagService.GetDistinctTags(q);
            var suggestions = SelectListHelper.GetSelectItemList(tags, x => x, x => x).OrderBy(x => x.Text).ToList();
            return R.Success.With("suggestions", suggestions).Result;
        }
    }
}