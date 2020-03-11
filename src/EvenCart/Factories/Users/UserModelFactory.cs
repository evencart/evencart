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

using EvenCart.Data.Entity.MediaEntities;
using EvenCart.Data.Entity.Settings;
using EvenCart.Data.Entity.Users;
using EvenCart.Infrastructure;
using EvenCart.Infrastructure.MediaServices;
using EvenCart.Infrastructure.Mvc.ModelFactories;
using EvenCart.Models.Users;
using EvenCart.Services.MediaServices;

namespace EvenCart.Factories.Users
{
    public class UserModelFactory : IUserModelFactory
    {
        private readonly IModelMapper _modelMapper;
        private readonly UserSettings _userSettings;
        private readonly IMediaAccountant _mediaAccountant;
        private readonly IMediaService _mediaService;
        public UserModelFactory(IModelMapper modelMapper, UserSettings userSettings, IMediaAccountant mediaAccountant, IMediaService mediaService)
        {
            _modelMapper = modelMapper;
            _userSettings = userSettings;
            _mediaAccountant = mediaAccountant;
            _mediaService = mediaService;
        }

        public UserModel Create(User user)
        {
            var model = _modelMapper.Map<UserModel>(user);
            model.CanChangeProfilePicture = _userSettings.AreProfilePicturesEnabled;

            Media media = null;
            if (user.ProfilePictureId.HasValue && user.ProfilePictureId > 0)
            {
                media = _mediaService.Get(user.ProfilePictureId.Value);
            }
            model.ProfilePictureUrl = _mediaAccountant.GetPictureUrl(media, ApplicationEngine.ActiveTheme.UserProfileImageSize, true);
            return model;
        }

        public UserMiniModel CreateMini(User user)
        {
            var model = new UserMiniModel()
            {
                Name = user.Name,
                Id = user.Id,
                Points = user.Points,
                CreatedOn = user.CreatedOn
            };
            Media media = null;
            if (user.ProfilePictureId.HasValue && user.ProfilePictureId > 0)
            {
                media = _mediaService.Get(user.ProfilePictureId.Value);
            }
            model.ProfilePictureUrl = _mediaAccountant.GetPictureUrl(media, ApplicationEngine.ActiveTheme.UserProfileImageSize, true);
            return model;
        }
    }
}