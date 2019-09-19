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
            if (user.ProfilePictureId.HasValue && user.ProfilePictureId > 0)
            {
                var media = _mediaService.Get(user.ProfilePictureId.Value);
                model.ProfilePictureUrl = _mediaAccountant.GetPictureUrl(media, ApplicationEngine.ActiveTheme.UserProfileImageSize, true);
            }
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
            if (user.ProfilePictureId.HasValue && user.ProfilePictureId > 0)
            {
                var media = _mediaService.Get(user.ProfilePictureId.Value);
                model.ProfilePictureUrl = _mediaAccountant.GetPictureUrl(media, ApplicationEngine.ActiveTheme.UserProfileImageSize, true);
            }

            return model;
        }
    }
}