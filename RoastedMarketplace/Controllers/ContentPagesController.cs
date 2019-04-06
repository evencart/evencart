using Microsoft.AspNetCore.Mvc;
using RoastedMarketplace.Data.Entity.Pages;
using RoastedMarketplace.Data.Entity.Settings;
using RoastedMarketplace.Infrastructure;
using RoastedMarketplace.Infrastructure.Helpers;
using RoastedMarketplace.Infrastructure.Mvc;
using RoastedMarketplace.Infrastructure.Mvc.ModelFactories;
using RoastedMarketplace.Infrastructure.Routing;
using RoastedMarketplace.Models.Pages;
using RoastedMarketplace.Services.Extensions;
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

        [HttpGet("page-preview/{id}", Name = RouteNames.PreviewPage)]
        public IActionResult Preview(int id)
        {
            var url = ApplicationEngine.RouteUrl(RouteNames.SinglePage, new { id = id });
            return Redirect(url);
        }

        [DynamicRoute(Name = RouteNames.SinglePage, SettingName = nameof(UrlSettings.ContentPageUrlTemplate), SeoEntityName = nameof(ContentPage))]
        public IActionResult Index(int id)
        {
            var contentPage = _contentPageService.Get(id);
            if (contentPage == null || (!contentPage.Published && !CurrentUser.IsAdministrator()))
                return NotFound();
            var contentPageModel = _modelMapper.Map<ContentPageModel>(contentPage);
            SeoMetaHelper.SetSeoData(contentPageModel.Name);
            return R.Success.With("contentPage", contentPageModel).With("contentPageId", contentPage.Id).With("preview", !contentPage.Published).Result;
        }

        [DualGet("contentpages/{contentPageId}", Name = RouteNames.SinglePage, OnlyApi = true)]
        public IActionResult IndexApi(int contentPageId)
        {
            return Index(contentPageId);
        }
    }
}