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

using EvenCart.Data.Entity.Pages;
using EvenCart.Data.Entity.Settings;
using EvenCart.Data.Extensions;
using EvenCart.Services.Extensions;
using EvenCart.Services.Pages;
using EvenCart.Infrastructure;
using EvenCart.Infrastructure.Extensions;
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
            if (!ApplicationEngine.CurrentLanguage.PrimaryLanguage)
            {
                //we need translations
                contentPage.PopulateTranslationsFromDb(ApplicationEngine.CurrentLanguage.CultureCode, true);
            }
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
        [DualGet("{contentPageId}", Name = RouteNames.SinglePage, OnlyApi = true)]
        public IActionResult IndexApi(int contentPageId)
        {
            return Index(contentPageId);
        }
    }
}