using Microsoft.AspNetCore.Mvc;
using RoastedMarketplace.Data.Entity.Pages;
using RoastedMarketplace.Data.Entity.Settings;
using RoastedMarketplace.Infrastructure.Helpers;
using RoastedMarketplace.Infrastructure.Mvc;
using RoastedMarketplace.Infrastructure.Mvc.ModelFactories;
using RoastedMarketplace.Infrastructure.Routing;
using RoastedMarketplace.Models.Pages;
using RoastedMarketplace.Services.Pages;

namespace RoastedMarketplace.Controllers
{
    public class ContentPagesController : FoundationController
    {
        private readonly IContentPageService _contentPageService;
        private readonly IModelMapper _modelMapper;
        public ContentPagesController(IContentPageService contentPageService, IModelMapper modelMapper)
        {
            _contentPageService = contentPageService;
            _modelMapper = modelMapper;
        }

        [DynamicRoute(Name = RouteNames.SinglePage, SettingName = nameof(UrlSettings.ContentPageUrlTemplate), SeoEntityName = nameof(ContentPage))]
        public IActionResult Index(int id)
        {
            var contentPage = _contentPageService.Get(id);
            if (contentPage == null)
                return NotFound();
            var contentPageModel = _modelMapper.Map<ContentPageModel>(contentPage);
            SeoMetaHelper.SetSeoData(contentPageModel.Name);
            return R.Success.With("contentPage", contentPageModel).With("contentPageId", contentPage.Id).Result;
        }
    }
}