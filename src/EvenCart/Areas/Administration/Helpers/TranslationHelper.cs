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

using System;
using System.Collections.Generic;
using System.Linq;
using Genesis;
using Genesis.Data;
using Genesis.Extensions;
using Genesis.Infrastructure.Mvc.Models;

namespace EvenCart.Areas.Administration.Helpers
{
    public static class TranslationHelper
    {
        public static void PopulateTranslations(GenesisModel model, IMultilingualEntity entity)
        {
            var multilingualFields = entity.GetType().GetProperties()
                .Where(x => x.IsDefined(typeof(MultilingualFieldAttribute), false)).ToList();
            var languages = GenesisEngine.Instance.AllLanguages.Where(x => !x.PrimaryLanguage);
            entity.PopulateTranslationsFromDb();
            foreach (var field in multilingualFields)
            {
                var translations = new List<dynamic>();
                foreach (var language in languages)
                {
                    var content = entity.Translations
                        .FirstOrDefault(x => x.CultureCode == language.CultureCode && x.FieldName.Equals(field.Name, StringComparison.InvariantCultureIgnoreCase))
                        ?.Content;

                    translations.Add(new { content = content, code = language.CultureCode, languageName = language.Name });
                }
                model.Translations.Set(field.Name.ToCamelCase(), translations);
            }
        }
    }
}