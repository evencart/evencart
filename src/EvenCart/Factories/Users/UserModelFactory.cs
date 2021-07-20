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

using EvenCart.Genesis.Modules.Users;
using EvenCart.Models.Users;
using Genesis;
using Genesis.Infrastructure;
using Genesis.Infrastructure.Mvc.ModelFactories;
using Genesis.MediaServices;
using Genesis.Modules.MediaServices;
using Genesis.Modules.Settings;
using Genesis.Modules.Users;

namespace EvenCart.Factories.Users
{
    public class UserModelFactory : IUserModelFactory
    {
        private readonly IModelMapper _modelMapper;
        private readonly UserSettings _userSettings;
        private readonly IMediaAccountant _mediaAccountant;
        private readonly IMediaService _mediaService;
        private readonly IGenesisEngine _applicationEngine;
        public UserModelFactory(IModelMapper modelMapper, UserSettings userSettings, IMediaAccountant mediaAccountant, IMediaService mediaService, IGenesisEngine applicationEngine)
        {
            _modelMapper = modelMapper;
            _userSettings = userSettings;
            _mediaAccountant = mediaAccountant;
            _mediaService = mediaService;
            _applicationEngine = applicationEngine;
        }

        public UserModel Create(User user)
        {
            var model = _modelMapper.Map<UserModel>(user);
            model.CanChangeProfilePicture = _userSettings.AreProfilePicturesEnabled;

            Media media = null;
            var userProfile = user.GetProfile();
            if (userProfile.ProfilePictureId.HasValue && userProfile.ProfilePictureId > 0)
            {
                media = _mediaService.Get(userProfile.ProfilePictureId.Value);
            }
            model.ProfilePictureUrl = _mediaAccountant.GetPictureUrl(media, GenesisEngine.Instance.ActiveTheme.UserProfileImageSize, true);
            return model;
        }

        public UserMiniModel CreateMini(User user)
        {
            var userProfile = user.GetProfile();
            var model = new UserMiniModel()
            {
                Name = userProfile.Name,
                Id = user.Id,
                Points = user.Points,
                CreatedOn = user.CreatedOn
            };
            Media media = null;
            if (userProfile.ProfilePictureId.HasValue && userProfile.ProfilePictureId > 0)
            {
                media = _mediaService.Get(userProfile.ProfilePictureId.Value);
            }
            model.ProfilePictureUrl = _mediaAccountant.GetPictureUrl(media, GenesisEngine.Instance.ActiveTheme.UserProfileImageSize, true);
            return model;
        }
    }
}