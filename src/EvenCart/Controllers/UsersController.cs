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

using EvenCart.Models.Media;
using EvenCart.Models.Users;
using Genesis.Infrastructure.Mvc;
using Genesis.Infrastructure.Mvc.Attributes;
using Genesis.Infrastructure.Mvc.ModelFactories;
using Genesis.MediaServices;
using Genesis.Modules.MediaServices;
using Genesis.Modules.Settings;
using Genesis.Modules.Users;
using Genesis.Routing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EvenCart.Controllers
{
    /// <summary>
    /// Allows authenticated user to manage their personal info
    /// </summary>
    [Route("users")]
    [Authorize]
    public class UsersController : GenesisController
    {
        private readonly IUserService _userService;
        private readonly IMediaAccountant _mediaAccountant;
        private readonly UserSettings _userSettings;
        private readonly IMediaService _mediaService;
        private readonly IModelMapper _modelMapper;
        private readonly AffiliateSettings _affiliateSettings;
        public UsersController(IUserService userService, IMediaAccountant mediaAccountant, UserSettings userSettings, IMediaService mediaService, IModelMapper modelMapper, AffiliateSettings affiliateSettings)
        {
            _userService = userService;
            _mediaAccountant = mediaAccountant;
            _userSettings = userSettings;
            _mediaService = mediaService;
            _modelMapper = modelMapper;
            _affiliateSettings = affiliateSettings;
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
            var currentUser = Engine.CurrentUser;
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
            model.ThumbnailUrl = _mediaAccountant.GetPictureUrl(media, Engine.ActiveTheme.UserProfileImageSize, true);
            model.ImageUrl = _mediaAccountant.GetPictureUrl(media);
            return R.Success.With("media", model).Result;
        }
        /// <summary>
        /// Sends request for logged in user to become an affiliate
        /// </summary>
        /// <response code="200">A success response object</response>
        [DualPost("affiliate", Name = RouteNames.RequestAffiliate, OnlyApi = true)]
        public IActionResult RequestAffiliateAccount()
        {
            if (CurrentUser.IsAffiliate)
                return R.Fail.With("error", T("The current user is already an affiliate")).Result;
            CurrentUser.IsAffiliate = true;
            CurrentUser.AffiliateActive = _affiliateSettings.AutoActivateAffiliateAccount;
            _userService.Update(CurrentUser);
            return R.Success.Result;
        }
    }
}