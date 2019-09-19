using EvenCart.Data.Entity.Settings;
using EvenCart.Services.Users;
using EvenCart.Infrastructure;
using EvenCart.Infrastructure.MediaServices;
using EvenCart.Infrastructure.Mvc;
using EvenCart.Infrastructure.Mvc.Attributes;
using EvenCart.Infrastructure.Mvc.ModelFactories;
using EvenCart.Infrastructure.Routing;
using EvenCart.Models.Media;
using EvenCart.Models.Users;
using EvenCart.Services.MediaServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EvenCart.Controllers
{
    /// <summary>
    /// Allows authenticated user to manage their personal info
    /// </summary>
    [Route("users")]
    [Authorize]
    public class UsersController : FoundationController
    {
        private readonly IUserService _userService;
        private readonly IMediaAccountant _mediaAccountant;
        private readonly UserSettings _userSettings;
        private readonly IMediaService _mediaService;
        private readonly IModelMapper _modelMapper;
        public UsersController(IUserService userService, IMediaAccountant mediaAccountant, UserSettings userSettings, IMediaService mediaService, IModelMapper modelMapper)
        {
            _userService = userService;
            _mediaAccountant = mediaAccountant;
            _userSettings = userSettings;
            _mediaService = mediaService;
            _modelMapper = modelMapper;
        }

        /// <summary>
        /// Saves authenticated user's information
        /// </summary>
        /// <param name="userModel"></param>
        /// <response code="200">A success response object</response>
        [ValidateModelState(ModelType = typeof(UserModel))]
        [DualPost("", Name = RouteNames.SaveUser, OnlyApi = true)]
        public IActionResult SaveUser(UserModel userModel)
        {
            var currentUser = ApplicationEngine.CurrentUser;
            //set the updated fields
            currentUser.FirstName = userModel.FirstName;
            currentUser.LastName = userModel.LastName;
            currentUser.Name = $"{currentUser.FirstName} {currentUser.LastName}";
            currentUser.CompanyName = userModel.CompanyName;
            currentUser.DateOfBirth = userModel.DateOfBirth;
            currentUser.MobileNumber = userModel.MobileNumber;
            currentUser.TimeZoneId = userModel.TimeZoneId;
            _userService.Update(currentUser);

            return R.Success.Result;
        }

        /// <summary>
        /// Handles profile image upload for user
        /// </summary>
        /// <param name="mediaModel"></param>
        /// <response code="200">A <see cref="MediaModel">media</see> object as media</response>
        [ValidateModelState(ModelType = typeof(MediaUploadModel))]
        [DualPost("avatar", Name = RouteNames.SaveUserPicture, OnlyApi = true)]
        public IActionResult UploadProfilePicture(MediaUploadModel mediaModel)
        {
            if (!_userSettings.AreProfilePicturesEnabled)
                return NotFound();

            var mediaFile = mediaModel.MediaFile;
            if (mediaFile == null || mediaFile.Length == 0)
            {
                return R.Fail.Result;
            }

            var media = _mediaAccountant.GetMediaInstance(mediaFile, 0);
            //save it
            _mediaService.Insert(media);
            _userService.Update(new { ProfilePictureId = media.Id }, x => x.Id == CurrentUser.Id, null);

            var model = _modelMapper.Map<MediaModel>(media);
            model.ThumbnailUrl = _mediaAccountant.GetPictureUrl(media, ApplicationEngine.ActiveTheme.UserProfileImageSize, true);
            model.ImageUrl = _mediaAccountant.GetPictureUrl(media);
            return R.Success.With("media", model).Result;
        }
    }
}