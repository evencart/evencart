using System;
using System.Collections.Generic;
using System.Linq;
using EvenCart.Core.Plugins;
using EvenCart.Infrastructure.MediaServices;
using EvenCart.Infrastructure.Mvc;
using EvenCart.Infrastructure.Mvc.ModelFactories;
using Microsoft.AspNetCore.Mvc;
using Ui.Slider.Models;
using Ui.Slider.Services;

namespace Ui.Slider.Components
{
    [ViewComponent(Name = WidgetSystemName)]
    public class SliderWidget : FoundationComponent, IWidget
    {
        private readonly IUiSliderService _uiSliderService;
        private readonly IModelMapper _modelMapper;
        private readonly IMediaAccountant _mediaAccountant;
        public SliderWidget(IUiSliderService uiSliderService, IModelMapper modelMapper, IMediaAccountant mediaAccountant)
        {
            _uiSliderService = uiSliderService;
            _modelMapper = modelMapper;
            _mediaAccountant = mediaAccountant;
        }
        public const string WidgetSystemName = "SliderWidget";
        public override IViewComponentResult Invoke(object data = null)
        {
            var slides = _uiSliderService.Get(x => x.Visible);
            var slideModels = slides.Select(x =>
            {
                var model = _modelMapper.Map<UiSliderModel>(x);
                model.ImageUrl = _mediaAccountant.GetPictureUrl(x.Media);
                return model;
            });
            return R.Success.With("slides", slideModels).ComponentResult;
        }

        public string DisplayName => "Slider";

        public string SystemName => WidgetSystemName;

        public IList<string> WidgetZones { get;  }  = new List<string>() { "slider" };

        public bool HasConfiguration { get; } = false;

        public bool SkipDragging { get; } = true;

        public string ConfigurationUrl { get; } = null;

        public Type SettingsType { get; } = null;

        public object GetViewObject(object settings)
        {
            return null;
        }
    }
}