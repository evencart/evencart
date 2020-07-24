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
using EvenCart.Core.Services;
using EvenCart.Core.Services.Events;
using EvenCart.Infrastructure.Extensions;
using EvenCart.Services.Cultures;

namespace EvenCart.Infrastructure.Consumers
{
    public class MultilingualEntityConsumer<T> : IFoundationEntityInserted<T>, IFoundationEntityUpdated<T>, IFoundationEntityDeleted<T> where T : FoundationEntity
    {
       
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
                mEntity.PopulateTranslationsFromForm();
                var translationData = mEntity.Translations;
                _translationService.Insert(translationData.ToArray());
            }
        }

        public void OnUpdated(T entity)
        {
            if (entity is IMultilingualEntity mEntity)
            {
                //first load original translations
                mEntity.PopulateTranslationsFromDb();
                //overwrite with the new translations
                mEntity.PopulateTranslationsFromForm();
                var translationData = mEntity.Translations;
                Transaction.Initiate(transaction =>
                {
                    foreach (var td in translationData)
                    {
                        _translationService.InsertOrUpdate(td, transaction);
                    }
                });
            }
        }

        public void OnDeleted(T entity)
        {
            if (entity is IMultilingualEntity mEntity && !(entity is ISoftDeletable))
            {
                _translationService.Delete(x => x.Guid == mEntity.TranslationGuid);
            }
        }
    }
}