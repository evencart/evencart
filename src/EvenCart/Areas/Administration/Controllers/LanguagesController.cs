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
using EvenCart.Areas.Administration.Models.Cultures;
using EvenCart.Data.Constants;
using EvenCart.Data.Entity.Cultures;
using EvenCart.Infrastructure.Mvc;
using EvenCart.Infrastructure.Mvc.Attributes;
using EvenCart.Infrastructure.Mvc.ModelFactories;
using EvenCart.Infrastructure.Routing;
using EvenCart.Infrastructure.Security.Attributes;
using EvenCart.Services.Cultures;
using Microsoft.AspNetCore.Mvc;

namespace EvenCart.Areas.Administration.Controllers
{
    public class LanguagesController : FoundationAdminController
    {
        private readonly ILanguageService _languageService;
        private readonly IModelMapper _modelMapper;
        public LanguagesController(ILanguageService languageService, IModelMapper modelMapper)
        {
            _languageService = languageService;
            _modelMapper = modelMapper;
        }

        [DualGet("", Name = AdminRouteNames.LanguagesList)]
        [CapabilityRequired(CapabilitySystemNames.ManageLanguage)]
        public IActionResult LanguagesList()
        {
            var languages = _languageService.Get(x => true);

            var languagesModel = languages.Select(x => _modelMapper.Map<LanguageModel>(x)).ToList();
            return R.Success.With("languages", languagesModel)
                .WithGridResponse(languagesModel.Count, 1, languagesModel.Count)
                .Result;
        }

        [DualGet("{languageId}", Name = AdminRouteNames.GetLanguage)]
        [CapabilityRequired(CapabilitySystemNames.ManageLanguage)]
        public IActionResult LanguageEditor(int languageId)
        {
            var language = languageId > 0 ? _languageService.Get(languageId) : new Language();
            if (language == null)
                return NotFound();
            var model = _modelMapper.Map<LanguageModel>(language);
            return R.Success.With("language", model).WithAllFlags().WithCultures().Result;
        }

        [DualPost("", Name = AdminRouteNames.SaveLanguage)]
        [CapabilityRequired(CapabilitySystemNames.ManageLanguage)]
        [ValidateModelState(ModelType = typeof(LanguageModel))]
        public IActionResult SaveLanguage(LanguageModel languageModel)
        {
            var language = languageModel.Id > 0 ? _languageService.Get(languageModel.Id) : new Language();
            if (language == null)
                return NotFound();

            if (languageModel.PrimaryLanguage && !language.PrimaryLanguage)
            {
                //remove default from other languages
                _languageService.Update(new {PrimaryLanguage = true}, x => true, null);
            }
            language.Name = languageModel.Name;
            language.Flag = languageModel.Flag;
            language.Rtl = languageModel.Rtl;
            language.Published = languageModel.Published;
            language.CultureCode = languageModel.CultureCode;
            language.PrimaryLanguage = languageModel.PrimaryLanguage;

            _languageService.InsertOrUpdate(language);
            return R.Success.With("id", language.Id).Result;
        }

        [DualPost("delete", Name = AdminRouteNames.DeleteLanguage)]
        [CapabilityRequired(CapabilitySystemNames.ManageLanguage)]
        public IActionResult DeleteLanguage(int languageId)
        {
            var language = languageId > 0 ? _languageService.Get(languageId) : new Language();
            if (language == null)
                return NotFound();

            _languageService.Delete(language);
            return R.Success.Result;
        }
    }
}