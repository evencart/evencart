using EvenCart.Data.Entity.Pages;
using EvenCart.Data.Entity.Settings;
using EvenCart.Data.Extensions;
using EvenCart.Services.Extensions;
using EvenCart.Services.Pages;
using EvenCart.Infrastructure;
using EvenCart.Infrastructure.Helpers;
using EvenCart.Infrastructure.Mvc;
using EvenCart.Infrastructure.Mvc.ModelFactories;
using EvenCart.Infrastructure.Routing;
using EvenCart.Models.Pages;
using Microsoft.AspNetCore.Mvc;

namespace EvenCart.Controllers
{
    /// <summary>
    /// Allows users to get the content pages
    /// </summary>
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
            var r = R.Success.With("contentPage", contentPageModel).With("contentPageId", contentPage.Id)
                .With("preview", !contentPage.Published);
            if (!contentPage.Template.IsNullEmptyOrWhiteSpace())
            {
                //get the template
                var template = ApplicationEngine.ActiveTheme.GetTemplatePath(contentPage.Template);
                if (!template.IsNullEmptyOrWhiteSpace())
                    r.WithView(template);
            }
            return r.Result;
        }

        /// <summary>
        /// Gets the content page with provided identifier
        /// </summary>
        /// <param name="contentPageId">The id of the page to retrieve.</param>
        /// <response code="200">The <see cref="ContentPageModel">contentPage</see> object.</response>
        [DualGet("contentpages/{contentPageId}", Name = RouteNames.SinglePage, OnlyApi = true)]
        public IActionResult IndexApi(int contentPageId)
        {
            return Index(contentPageId);
        }
    }
}