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

using EvenCart.Areas.Administration.Models.Pages;
using Genesis.Data;
using Genesis.Extensions;
using Genesis.Modules.Web;

namespace EvenCart.Areas.Administration.Extensions
{
    public static class SeoMetaExtensions
    {
        public static void UpdateSeoMetaForEntity<T>(this ISeoMetaService seoMetaService, T entity, SeoMetaModel seoMetaModel) where T : GenesisEntity
        {
            var seoMeta = seoMetaService.FirstOrDefault(
                x => x.EntityId == entity.Id && x.EntityName == typeof(T).Name);
            if (seoMeta != null)
            {
                seoMeta.PageTitle = seoMetaModel.PageTitle;
                seoMeta.MetaKeywords = seoMetaModel.MetaKeywords;
                seoMeta.MetaDescription = seoMetaModel.MetaDescription;
                if (!seoMetaModel.Slug.IsNullEmptyOrWhiteSpace())
                {
                    //check if slug is safe to use, modify if required
                    var slug = seoMetaModel.Slug;
                    SeoMeta savedSeoMeta = null;
                    var index = 1;
                    while ((savedSeoMeta = seoMetaService.FirstOrDefault(x => x.EntityName == typeof(T).Name && x.Slug == slug && x.Id != seoMeta.Id)) != null)
                    {
                        slug = savedSeoMeta.Slug + (index++);
                    }
                    seoMeta.Slug = slug;
                }
                seoMetaService.Update(seoMeta);
            }
        }
    }
}