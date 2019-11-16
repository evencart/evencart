using System.Collections.Generic;
using System.Linq;
using EvenCart.Core.Services;
using EvenCart.Services.Serializers;
using EvenCart.Infrastructure.MediaServices;
using EvenCart.Infrastructure.Mvc;
using EvenCart.Infrastructure.Mvc.ModelFactories;
using EvenCart.Infrastructure.Routing;
using Microsoft.AspNetCore.Mvc;
using Ui.Slider.Data;
using Ui.Slider.Models;
using Ui.Slider.Services;

namespace Ui.Slider.Controllers
{
    public class UiSliderController : FoundationAdminController
    {
        private readonly IUiSliderService _uiSliderService;
        private readonly IModelMapper _modelMapper;
        private readonly IMediaAccountant _mediaAccountant;
        private readonly IDataSerializer _dataSerializer;
        public UiSliderController(IUiSliderService uiSliderService, IModelMapper modelMapper, IMediaAccountant mediaAccountant, IDataSerializer dataSerializer)
        {
            _uiSliderService = uiSliderService;
            _modelMapper = modelMapper;
            _mediaAccountant = mediaAccountant;
            _dataSerializer = dataSerializer;
        }

        [DualGet("configure", Name = UiSliderRouteNames.SliderConfigure)]
        public IActionResult Configure()
        {
            return R.Success.Result;
        }

        [DualGet("{slideId}", Name = UiSliderRouteNames.GetSlide, OnlyApi = true)]
        public IActionResult GetSlide(int slideId)
        {
            UiSlider slide = null;
            if (slideId <= 0 || (slide = _uiSliderService.Get(slideId)) == null)
                return NotFound();
            var model = _modelMapper.Map<UiSliderModel>(slide);
            model.ImageUrl = _mediaAccountant.GetPictureUrl(slide.Media);
            return R.Success.With("slide", model).Result;
        }

        [DualGet("list", Name = UiSliderRouteNames.SlidesList)]
        public IActionResult SlideList()
        {
            var sliders = _uiSliderService.Get(x => true).ToList();
            var models = sliders.Select(x =>
                {
                    var model = _modelMapper.Map<UiSliderModel>(x);
                    model.ImageUrl = _mediaAccountant.GetPictureUrl(x.Media);
                    return model;
                })
                .ToList();

            return R.Success.With("slides", models).Result;
        }

        [DualPost("", Name = UiSliderRouteNames.SaveSlide, OnlyApi = true)]
        public IActionResult SaveSlide(UiSliderModel slideModel)
        {
            UiSlider slide = null;
            if (slideModel.Id > 0 && (slide = _uiSliderService.Get(slideModel.Id)) == null)
            {
                return NotFound();
            }

            slide = slide ?? new UiSlider();
            slide.MediaId = slideModel.MediaId;
            slide.Title = slideModel.Title;
            slide.Visible = slideModel.Visible;
            slide.Url = slideModel.Url;
            slide.DisplayOrder = _uiSliderService.Count(x => true);

            _uiSliderService.InsertOrUpdate(slide);
            return R.Success.Result;
        }

        [DualPost("{slideId}/delete", Name = UiSliderRouteNames.SlideDelete, OnlyApi = true)]
        public IActionResult DeleteSlide(int slideId)
        {
            UiSlider slide = null;
            if (slideId >= 0 && (slide = _uiSliderService.Get(slideId)) == null)
            {
                return NotFound();
            }
            if (slide != null)
                _uiSliderService.Delete(slide);
            return R.Success.Result;
        }

        [DualPost("displayorder", Name = UiSliderRouteNames.UpdateSlideDisplayOrder, OnlyApi = true)]
        public IActionResult UpdateSlideOrder(IList<UiSliderModel> slideModels)
        {
            if (slideModels == null || !slideModels.Any())
                return R.Success.Result;
            var slideIds = slideModels.Select(x => x.Id).ToList();
            var slides = _uiSliderService.Get(x => slideIds.Contains(x.Id)).ToList();
            Transaction.Initiate(transaction =>
            {
                foreach (var slide in slides)
                {
                    slide.DisplayOrder = slideModels.FirstOrDefault(x => x.Id == slide.Id)?.DisplayOrder ??
                                         slide.DisplayOrder;
                    _uiSliderService.Update(slide, transaction);
                }
            });
            return R.Success.Result;
        }
    }
}