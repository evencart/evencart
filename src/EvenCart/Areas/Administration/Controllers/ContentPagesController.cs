﻿#region License
// Copyright (c) Sojatia Infocrafts Private Limited.
// The following code is part of EvenCart eCommerce Software (https://evencart.co) Dual Licensed under the terms of
// 
// 1. GNU GPLv3 with additional terms (available to read at https://evencart.co/license)
// 2. EvenCart Proprietary License (available to read at https://evencart.co/license/commercial-license).
// 
// You can select one of the above two licenses according to your requirements. The usage of this code is
// subject to the terms of the license chosen by you.
#endregion

using System;
using System.Linq;
using EvenCart.Areas.Administration.Extensions;
using EvenCart.Areas.Administration.Helpers;
using EvenCart.Areas.Administration.Models.Pages;
using EvenCart.Areas.Administration.Models.Users;
using Genesis;
using Genesis.Extensions;
using Genesis.Helpers;
using Genesis.Infrastructure.Mvc;
using Genesis.Infrastructure.Mvc.Attributes;
using Genesis.Infrastructure.Mvc.ModelFactories;
using Genesis.Infrastructure.Security.Attributes;
using Genesis.Modules.Data;
using Genesis.Modules.Web;
using Genesis.Routing;
using Microsoft.AspNetCore.Mvc;

namespace EvenCart.Areas.Administration.Controllers
{
    public class ContentPagesController : GenesisAdminController
    {
        private readonly IContentPageService _contentPageService;
        private readonly IModelMapper _modelMapper;
        private readonly IDataSerializer _dataSerializer;
        private readonly ISeoMetaService _seoMetaService;
        public ContentPagesController(IContentPageService contentPageService, IModelMapper modelMapper, IDataSerializer dataSerializer, ISeoMetaService seoMetaService)
        {
            _contentPageService = contentPageService;
            _modelMapper = modelMapper;
            _dataSerializer = dataSerializer;
            _seoMetaService = seoMetaService;
        }

        [DualGet("", Name = AdminRouteNames.ContentPagesList)]
        [CapabilityRequired(CapabilitySystemNames.ViewContentPages)]
        public IActionResult ContentPagesList(ContentPageSearchModel parameters)
        {
            var current = parameters?.Current ?? 1;
            var rowCount = parameters?.RowCount ?? 15;
            var contentPages =
                _contentPageService.GetContentPages(out int totalResults, parameters?.SearchPhrase, current, rowCount).ToList().GetWithParentTree();

            var contentPageModels = contentPages.Select(x =>
            {
                var model = PrepareModel(x);
                model.ParentPath = x.GetNameBreadCrumb();
                return model;
            }).Select(x =>
            {
                x.Content = ""; //no need to send content on list page...will save some bandwidth
                return x;
            }).ToList();
            return R.Success.WithGridResponse(totalResults, current, rowCount)
                .With("contentPages", contentPageModels)
                .WithParams(parameters)
                .Result;
        }
      
        [DualGet("{contentPageId}", Name = AdminRouteNames.GetContentPage)]
        [CapabilityRequired(CapabilitySystemNames.EditContentPage)]
        public IActionResult ContentPageEditor(int contentPageId)
        {
            var contentPage = contentPageId > 0 ? _contentPageService.Get(contentPageId) : new ContentPage();
            if (contentPage == null)
                return NotFound();
            var model = PrepareModel(contentPage);
            //add translations
            TranslationHelper.PopulateTranslations(model, contentPage);
         
            //get all pages to make a tree
            var availablePages = _contentPageService.Get(x => x.Id != contentPageId).ToList().GetWithParentTree();
            var contentPageModels =
                SelectListHelper.GetSelectItemListWithAction(availablePages, x => x.Id, x => x.GetNameBreadCrumb())
                    .OrderBy(x => x.Text).ToList();
            var storeIds = contentPage.Stores?.Select(x => x.Id).ToList();
            model.StoreIds = storeIds;
            return R.Success.With("contentPage", model)
                .With("availablePages", contentPageModels)
                .WithAvailableStores(storeIds)
                .WithActiveThemeTemplates()
                .Result;
        }

        [DualPost("", Name = AdminRouteNames.SaveContentPage, OnlyApi = true)]
        [CapabilityRequired(CapabilitySystemNames.EditContentPage)]
        [ValidateModelState(ModelType = typeof(ContentPageModel))]
        public IActionResult SaveContentPage(ContentPageModel model)
        {
            var contentPage = model.Id > 0 ? _contentPageService.Get(model.Id) : new ContentPage();
            if (contentPage == null)
                return NotFound();
            if (model.Published && (model.SeoMeta?.Slug.IsNullEmptyOrWhiteSpace() ?? true))
            {
                if (model.Id > 0)
                    return R.Fail.With("error", T("Can't publish page without slug")).Result;
            }
            _modelMapper.Map(model, contentPage, nameof(ContentPage.CreatedOn), nameof(ContentPage.PublishedOn), nameof(ContentPage.UserId));
            if (contentPage.Id == 0)
            {
                contentPage.CreatedOn = DateTime.UtcNow;
                contentPage.PublishedOn = model.PublishedOn == default(DateTime) ? DateTime.UtcNow : model.PublishedOn;
                contentPage.UserId = CurrentUser.Id;
            }
            contentPage.UpdatedOn = DateTime.UtcNow;
            contentPage.ParentId = model.ParentId;
            contentPage.StoreIds = model.StoreIds;
            _contentPageService.InsertOrUpdate(contentPage);

            //update the seometa
            _seoMetaService.UpdateSeoMetaForEntity(contentPage, model.SeoMeta);
            return R.Success.With("contentPageId", contentPage.Id).Result;
        }

        [DualPost("delete", Name = AdminRouteNames.DeleteContentPage, OnlyApi = true)]
        [CapabilityRequired(CapabilitySystemNames.DeleteContentPage)]
        public IActionResult DeleteContentPage(int contentPageId)
        {
            var contentPage = _contentPageService.Get(contentPageId);
            if (contentPage == null)
                return NotFound();
            _contentPageService.Delete(contentPage);
            return R.Success.Result;
        }

        #region Helpers

        private ContentPageModel PrepareModel(ContentPage page)
        {
            var model = _modelMapper.Map<ContentPageModel>(page);
            model.SeoMeta = _modelMapper.Map<SeoMetaModel>(page.SeoMeta);
            model.User = _modelMapper.Map<UserMiniModel>(page.User);
            return model;
        }
        #endregion

    }
}