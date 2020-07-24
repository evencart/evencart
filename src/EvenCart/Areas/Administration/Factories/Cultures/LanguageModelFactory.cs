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

using EvenCart.Areas.Administration.Models.Cultures;
using EvenCart.Data.Entity.Cultures;
using EvenCart.Infrastructure;

namespace EvenCart.Areas.Administration.Factories.Cultures
{
    public class LanguageModelFactory : ILanguageModelFactory
    {
        public LanguageModel Create(Language entity)
        {
            return new LanguageModel(){
                CultureCode = entity.CultureCode,
                Flag = entity.Flag,
                Name = entity.Name,
                PrimaryLanguage = entity.PrimaryLanguage,
                Published = entity.Published,
                Rtl = entity.Rtl,
                Id = entity.Id,
                FlagUrl = ApplicationEngine.MapUrl($"~/common/flags/{entity.Flag}")
        };
        }
    }
}