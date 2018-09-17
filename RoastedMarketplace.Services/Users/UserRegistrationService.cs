using System;
using RoastedMarketplace.Core;
using RoastedMarketplace.Data.Entity.Users;
using RoastedMarketplace.Data.Enum;
using RoastedMarketplace.Services.Security;

namespace RoastedMarketplace.Services.Users
{
    public class UserRegistrationService : IUserRegistrationService
    {
        private readonly IUserService _userService;
        private readonly ICryptographyService _cryptographyService;

        public UserRegistrationService(IUserService userService, 
            ICryptographyService cryptographyService)
        {
            _userService = userService;
            _cryptographyService = cryptographyService;
        }

        public UserRegistrationStatus Register(string email, string password, PasswordFormat passwordFormat)
        {
            //before registering the user, we need to check a few things

            //does the user exist already?
            var existingUser = _userService.FirstOrDefault(x => x.Email == email);
            if (existingUser != null)
                return UserRegistrationStatus.FailedAsEmailAlreadyExists;

            //we can create a user now, we'll need to hash the password
            var salt = _cryptographyService.CreateSalt(8); //64 bits...should be good enough

            var hashedPassword = _cryptographyService.GetHashedPassword(password, salt, passwordFormat);

            //create a new user entity
            var user = new User()
            {
                Email = email,
                DateCreated = DateTime.UtcNow,
                DateUpdated = DateTime.UtcNow,
                LastLoginDate = DateTime.UtcNow,
                LastLoginIpAddress = WebHelper.GetClientIpAddress(),
                Password = hashedPassword,
                PasswordSalt = salt,
                PasswordFormat = passwordFormat,
                Guid = Guid.NewGuid(),
                IsSystemAccount = false,
                Active = true
            };

            _userService.Insert(user);
            return UserRegistrationStatus.Success;
        }

        public UserRegistrationStatus Register(User user, PasswordFormat passwordFormat)
        {
            //does the user exist already?
            var existingUser = _userService.FirstOrDefault(x => x.Email == user.Email);
            if (existingUser != null)
                return UserRegistrationStatus.FailedAsEmailAlreadyExists;

            //we can create a user now, we'll need to hash the password
            var salt = _cryptographyService.CreateSalt(8); //64 bits...should be good enough

            var hashedPassword = _cryptographyService.GetHashedPassword(user.Password, salt, passwordFormat);

            user.Password = hashedPassword;
            user.PasswordSalt = salt;
            user.PasswordFormat = passwordFormat;
            _userService.Insert(user);
            return UserRegistrationStatus.Success;
        }
    }
}