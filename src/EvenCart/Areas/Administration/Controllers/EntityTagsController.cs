#region License
// Copyright (c) Sojatia Infocrafts Private Limited.
// The following code is part of EvenCart eCommerce Software (https://evencart.co) Dual Licensed under the terms of
// 
// 1. GNU GPLv3 with additional terms (available to read at https://evencart.co/license)
// 2. EvenCart Proprietary License (available to read at https://evencart.co/license/commercial-license).
// 
// You can select one of the above two licenses according to your requirements. The usage of this code is
// subject to the terms of the license chosen by you.
#endregion

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