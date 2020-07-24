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
using EvenCart.Core.Data;
using EvenCart.Core.Services.Events;
using EvenCart.Services.Cultures;

namespace EvenCart.Infrastructure.Consumers
{
    public class MultilingualEntityConsumer<T> : IFoundationEntityInserted<T>, IFoundationEntityUpdated<T>, IFoundationEntityDeleted<T> where T : FoundationEntity
    {
        private const string FieldFormat = "translation.{0}.{1}"; //e.g. translation.en-US.title
        private readonly ITranslationService _translationService;
        private readonly ILanguageService _languageService;

        public MultilingualEntityConsumer(ITranslationService translationService, ILanguageService languageService)
        {
            _translationService = translationService;
            _languageService = languageService;
        }

        public void OnInserted(T entity)
        {
            if (entity is IMultilingualEntity mEntity)
            {
                var multilingualFields = typeof(T).GetProperties()
                    .Where(x => x.IsDefined(typeof(MultilingualFieldAttribute), false)).ToList();
                ////var publishedLanguages = 
                foreach (var mF in multilingualFields)
                {
                    
                }
            }
        }

        public void OnUpdated(T entity)
        {
            if (entity is IMultilingualEntity mEntity)
            {

            }
        }

        public void OnDeleted(T entity)
        {
            if (entity is IMultilingualEntity mEntity)
            {

            }
        }
    }
}