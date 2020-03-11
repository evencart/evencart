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
using EvenCart.Areas.Administration.Models.Users;
using EvenCart.Data.Entity.Users;
using EvenCart.Data.Extensions;
using EvenCart.Infrastructure.Mvc.ModelFactories;

namespace EvenCart.Areas.Administration.Factories.Users
{
    public class UserModelFactory : IUserModelFactory
    {
        private readonly IModelMapper _modelMapper;

        public UserModelFactory(IModelMapper modelMapper)
        {
            _modelMapper = modelMapper;
        }

        public UserModel Create(User user)
        {

            var userModel = _modelMapper.Map<UserModel>(user);

            var roles = user.Roles;
            //set default role to registered
            if (user.Id == 0)
            {
                userModel.Roles = roles?.Where(x => x.SystemName == SystemRoleNames.Registered).Select(x => _modelMapper.Map<RoleModel>(x)).ToList();
            }
            else
            {
                userModel.Roles = user.Roles?.Select(x => _modelMapper.Map<RoleModel>(x)).ToList();
            }
            return userModel;
        }

        public UserMiniModel CreateMini(User entity)
        {
            var model = new UserMiniModel()
            {
                Name = entity.Name,
                Id = entity.Id
            };
            if (model.Name.IsNullEmptyOrWhiteSpace())
                model.Name = entity.Email;
            return model;
        }

        public UserPointModel Create(UserPoint entity)
        {
            var model = _modelMapper.Map<UserPointModel>(entity);
            if (entity.ActivatorUser != null)
                model.ActivatorUser = CreateMini(entity.ActivatorUser);
            return model;
        }

        public StoreCreditModel Create(StoreCredit entity)
        {
            var model = new StoreCreditModel()
            {
                Id = entity.Id,
                UserId = entity.UserId,
                Description = entity.Description,
                CreatedOn = entity.CreatedOn,
                AvailableOn = entity.AvailableOn,
                Credit = entity.Credit,
                ExpiresOn = entity.ExpiresOn
            };
            return model;
        }
    }
}