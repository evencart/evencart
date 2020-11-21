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
using System.Text.RegularExpressions;
using EvenCart.Core.Data;
using EvenCart.Core.Infrastructure;
using EvenCart.Data.Extensions;
using EvenCart.Services.Cultures;

namespace EvenCart.Infrastructure.Extensions
{
    public static class TranslationDataExtensions
    {
        public const string FieldFormat = "translation.{0}.{1}"; //e.g. translation.en-US.Title

        public static Regex TranslatedFieldRegex = new Regex(@"translation\.([^\.]+)\.([^\.]+)");

        public static void PopulateTranslationsFromForm(this IMultilingualEntity entity)
        {
            var multilingualFields = entity.GetType().GetProperties()
                .Where(x => x.IsDefined(typeof(MultilingualFieldAttribute), false)).ToList();
            entity.Translations = entity.Translations ?? new List<TranslationData>();
            foreach (var item in ApplicationEngine.CurrentHttpContext.Request.Form)
            {
                if (TranslatedFieldRegex.IsMatch(item.Key))
                {
                    var match = TranslatedFieldRegex.Match(item.Key);
                    var cultureCode = match.Groups[1].Value;
                    var fieldName = match.Groups[2].Value;
                    if (multilingualFields.All(x => !x.Name.Equals(fieldName, StringComparison.InvariantCultureIgnoreCase)))
                        continue;

                    var tData = entity.Translations.FirstOrDefault(x =>
                                    x.FieldName.Equals(fieldName, StringComparison.InvariantCultureIgnoreCase) &&
                                    x.CultureCode == cultureCode) ?? new TranslationData();

                    if (tData.Id == 0)
                        entity.Translations.Add(tData);

                    tData.FieldName = fieldName;
                    tData.Content = item.Value.ToString();
                    tData.Guid = entity.TranslationGuid;
                    tData.CultureCode = cultureCode;
                    if (tData.Guid.IsNullEmptyOrWhiteSpace())
                        tData.Guid = Guid.NewGuid().ToString();
                }
            }
        }

        public static void PopulateTranslationsFromDb(this IMultilingualEntity entity)
        {
            var translationService = DependencyResolver.Resolve<ITranslationService>();
            entity.Translations = translationService.Get(x => x.Guid == entity.TranslationGuid).ToList();
        }

        public static void PopulateTranslationsFromDb(this IMultilingualEntity entity, string cultureCode, bool replaceOriginals = false)
        {
            var translationService = DependencyResolver.Resolve<ITranslationService>();
            entity.Translations = translationService.Get(x => x.Guid == entity.TranslationGuid && x.CultureCode == cultureCode).ToList();
            if (replaceOriginals)
            {
                //get the fields
                var multilingualFields = entity.GetType().GetProperties()
                    .Where(x => x.IsDefined(typeof(MultilingualFieldAttribute), false)).ToList();
                foreach (var field in multilingualFields)
                {
                    var value = entity.Translations.FirstOrDefault(x =>
                        x.FieldName.Equals(field.Name, StringComparison.InvariantCultureIgnoreCase))?.Content;
                    if (!value.IsNullEmptyOrWhiteSpace())
                    {
                        field.SetValue(entity, value);
                    }
                }
            }
        }
    }


}