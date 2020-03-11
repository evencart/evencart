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
using EvenCart.Areas.Administration.Models.Media;
using EvenCart.Core.Services;
using EvenCart.Data.Constants;
using EvenCart.Services.MediaServices;
using EvenCart.Services.Products;
using EvenCart.Infrastructure;
using EvenCart.Infrastructure.MediaServices;
using EvenCart.Infrastructure.Mvc;
using EvenCart.Infrastructure.Mvc.Attributes;
using EvenCart.Infrastructure.Mvc.ModelFactories;
using EvenCart.Infrastructure.Routing;
using EvenCart.Infrastructure.Security.Attributes;
using EvenCart.Services.Users;
using Microsoft.AspNetCore.Mvc;

namespace EvenCart.Areas.Administration.Controllers
{
    public class MediaController : FoundationAdminController
    {
        private readonly IMediaAccountant _mediaAccountant;
        private readonly IMediaService _mediaService;
        private readonly IModelMapper _modelMapper;
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IUserService _userService;
        public MediaController(IMediaAccountant mediaAccountant, IMediaService mediaService, IModelMapper modelMapper, IProductService productService, ICategoryService categoryService, IUserService userService)
        {
            _mediaAccountant = mediaAccountant;
            _mediaService = mediaService;
            _modelMapper = modelMapper;
            _productService = productService;
            _categoryService = categoryService;
            _userService = userService;
        }

        [DualPost("upload", Name = AdminRouteNames.UploadMedia, OnlyApi = true)]
        [CapabilityRequired(CapabilitySystemNames.UploadMedia)]
        [ValidateModelState(ModelType = typeof(MediaUploadModel))]
        public IActionResult UploadMedia(MediaUploadModel mediaModel)
        {
            var mediaFile = mediaModel.MediaFile;
            if (mediaFile == null || mediaFile.Length == 0)
            {
                return R.Fail.Result;
            }

            var media = _mediaAccountant.GetMediaInstance(mediaFile, 0);
            //save it
            _mediaService.Insert(media);
            //link media if we can
            if (mediaModel.EntityId > 0)
            {
                switch (mediaModel.EntityName)
                {
                    case "product":
                        if (_productService.Count(x => x.Id == mediaModel.EntityId) > 0)
                        {
                            _productService.LinkMediaWithProduct(media.Id, mediaModel.EntityId);
                        }
                        break;
                    case "category":
                        if (_categoryService.Count(x => x.Id == mediaModel.EntityId) > 0)
                        {
                            _categoryService.Update(new {MediaId = media.Id}, x => x.Id == mediaModel.EntityId, null);
                        }
                        break;
                    case "user":
                        if (_userService.Count(x => x.Id == mediaModel.EntityId) > 0)
                        {
                            _userService.Update(new { ProfilePictureId = media.Id }, x => x.Id == mediaModel.EntityId, null);
                        }
                        break;
                }
            }
            //model
            var model = _modelMapper.Map<MediaModel>(media);
            model.ThumbnailUrl = _mediaAccountant.GetPictureUrl(media, ApplicationConfig.AdminThumbnailWidth,
                ApplicationConfig.AdminThumbnailHeight);
            model.ImageUrl = _mediaAccountant.GetPictureUrl(media);
            return R.Success.With("media", model).Result;
        }

        [DualPost("displayorder", Name = AdminRouteNames.UpdateMediaDisplayOrder, OnlyApi = true)]
        [CapabilityRequired(CapabilitySystemNames.UploadMedia)]
        public IActionResult UpdateDisplayOrders(MediaModel[] media)
        {
            if (media == null)
                return BadRequest();

            //get media models with no-zero ids
            var validMediaModels = media.Where(x => x.Id != 0);
            Transaction.Initiate(transaction =>
            {
                foreach (var model in validMediaModels)
                {
                    _mediaService.Update(new {DisplayOrder = model.DisplayOrder}, m => m.Id == model.Id,
                        transaction);
                }
            });
            return R.Success.Result;
        }
        [DualPost("delete", Name = AdminRouteNames.DeleteMedia, OnlyApi = true)]
        [CapabilityRequired(CapabilitySystemNames.UploadMedia)]
        public IActionResult DeleteMedia(int mediaId)
        {
            var media = mediaId > 0 ? _mediaService.Get(mediaId) : null;
            if (media == null)
                return NotFound();
            _mediaService.Delete(media);
            return R.Success.Result;
        }
    }
}