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

using EvenCart.Areas.Administration.Models.Gdpr;
using EvenCart.Data.Entity.Gdpr;
using EvenCart.Infrastructure.Mvc.ModelFactories;

namespace EvenCart.Areas.Administration.Factories.Gdpr
{
    public class GdprModelFactory : IGdprModelFactory
    {
        private readonly IModelMapper _modelMapper;

        public GdprModelFactory(IModelMapper modelMapper)
        {
            _modelMapper = modelMapper;
        }

        public ConsentModel Create(Consent entity)
        {
            var model = _modelMapper.Map<ConsentModel>(entity);
            if (entity.ConsentGroup != null)
            {
                model.ConsentGroup = _modelMapper.Map<ConsentGroupModel>(entity.ConsentGroup);
            }
            return model;
        }

        public ConsentLogModel Create(ConsentLog entity)
        {
            var model = new ConsentLogModel()
            {
                ActivityType = entity.ActivityType,
                ConsentTitle = entity.Consent.Title,
                CreatedOn = entity.CreatedOn,
                UserInfo = entity.EncryptedUserInfo
            };
            return model;
        }
    }
}