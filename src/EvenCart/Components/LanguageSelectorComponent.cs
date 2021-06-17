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
using EvenCart.Areas.Administration.Factories.Cultures;
using Genesis.Infrastructure.Mvc;
using Genesis.Modules.Localization;
using Genesis.Modules.Settings;
using Microsoft.AspNetCore.Mvc;

namespace EvenCart.Components
{
    [ViewComponent(Name = "LanguageSelector")]
    public class LanguageSelectorComponent : GenesisComponent
    {
        private readonly ILanguageService _languageService;
        private readonly ILanguageModelFactory _languageModelFactory;
        private readonly LocalizationSettings _localizationSettings;
        public LanguageSelectorComponent(ILanguageService languageService, ILanguageModelFactory languageModelFactory, LocalizationSettings localizationSettings)
        {
            _languageService = languageService;
            _languageModelFactory = languageModelFactory;
            _localizationSettings = localizationSettings;
        }

        public override IViewComponentResult Invoke(object data = null)
        {
            if (!_localizationSettings.AllowUserToSelectLanguage)
                return R.Success.ComponentResult;

            var languages = Engine.AllLanguages.Where(x => x.Published).ToList();
            if (languages.Count < 2)
                return R.Success.ComponentResult; // no need to display the box at all if there are none or one language

            var models = languages.Select(_languageModelFactory.Create).ToList();
            var activeLanguage = _languageModelFactory.Create(Engine.CurrentLanguage);
            return R.Success.With("languages", models).With("activeLanguage", activeLanguage).ComponentResult;
        }
    }
}