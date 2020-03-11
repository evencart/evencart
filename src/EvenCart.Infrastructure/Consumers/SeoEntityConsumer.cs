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
using EvenCart.Core;
using EvenCart.Core.Data;
using EvenCart.Core.Services.Events;
using EvenCart.Data.Entity.Pages;
using EvenCart.Data.Extensions;
using EvenCart.Services.Pages;

namespace EvenCart.Infrastructure.Consumers
{
    public class SeoEntityConsumer<T> : IFoundationEntityInserted<T>, IFoundationEntityUpdated<T>, IFoundationEntityDeleted<T> where T: FoundationEntity
    {
        private readonly ISeoMetaService _seoMetaService;
        public SeoEntityConsumer(ISeoMetaService seoMetaService)
        {
            _seoMetaService = seoMetaService;
        }

        public void OnInserted(T entity)
        {
            if (!(entity is ISeoEntity))
                return;
            var name = ((ISeoEntity) entity).Name;
            var seoMeta = new SeoMeta()
            {
                EntityId = entity.Id,
                EntityName = typeof(T).Name,
                LanguageCultureCode = ApplicationEngine.CurrentLanguageCultureCode,
                Slug = CommonHelper.GenerateSlug(name),
                PageTitle = name
            };
            _seoMetaService.Insert(seoMeta);

            //is there a property to for seometa
            var property = typeof(T).GetProperties().FirstOrDefault(x => x.PropertyType == typeof(SeoMeta) && x.CanWrite);
            if (property != null)
            {
                property.SetValue(entity, seoMeta);
            }
        }

        public void OnDeleted(T entity)
        {
            if (!(entity is ISeoEntity))
                return;
            var seoMeta = _seoMetaService.GetForEntity<T>(entity.Id);
            if(seoMeta != null)
                _seoMetaService.Delete(seoMeta);
        }

        public void OnUpdated(T entity)
        {
            if (!(entity is ISeoEntity))
                return;
            var name = ((ISeoEntity)entity).Name;
            var seoMeta = _seoMetaService.GetForEntity<T>(entity.Id);
            if (seoMeta != null)
            {
                if (seoMeta.PageTitle.IsNullEmptyOrWhiteSpace())
                {
                    seoMeta.PageTitle = name;
                    _seoMetaService.Update(seoMeta);
                }
            }
        }
    }
}