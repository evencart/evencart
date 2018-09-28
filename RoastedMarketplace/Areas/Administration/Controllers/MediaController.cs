using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RoastedMarketplace.Areas.Administration.Models.Media;
using RoastedMarketplace.Core.Services;
using RoastedMarketplace.Data.Constants;
using RoastedMarketplace.Infrastructure;
using RoastedMarketplace.Infrastructure.MediaServices;
using RoastedMarketplace.Infrastructure.Mvc;
using RoastedMarketplace.Infrastructure.Mvc.Attributes;
using RoastedMarketplace.Infrastructure.Mvc.ModelFactories;
using RoastedMarketplace.Infrastructure.Routing;
using RoastedMarketplace.Infrastructure.Security.Attributes;
using RoastedMarketplace.Services.MediaServices;
using RoastedMarketplace.Services.Products;

namespace RoastedMarketplace.Areas.Administration.Controllers
{
    public class MediaController : FoundationAdminController
    {
        private readonly IMediaAccountant _mediaAccountant;
        private readonly IMediaService _mediaService;
        private readonly IModelMapper _modelMapper;
        private readonly IProductService _productService;
        public MediaController(IMediaAccountant mediaAccountant, IMediaService mediaService, IModelMapper modelMapper, IProductService productService)
        {
            _mediaAccountant = mediaAccountant;
            _mediaService = mediaService;
            _modelMapper = modelMapper;
            _productService = productService;
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
                }
            }
            //model
            var model = _modelMapper.Map<MediaModel>(media);
            model.ThumbnailUrl = _mediaAccountant.GetPictureUrl(media, ApplicationConfig.AdminThumbnailWidth,
                ApplicationConfig.AdminThumbnailHeight);
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
    }
}