using System;
using EvenCart.Infrastructure.Mvc.Models;
using EvenCart.Infrastructure.Mvc.Validator;
using FluentValidation;

namespace EvenCart.Models.Users
{
    public class UserModel : FoundationEntityModel, IRequiresValidations<UserModel>
    {
        /// <summary>
        /// The first name of the user
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// The last name of the user
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// The full name of the user. Ignored for POST requests.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The email of the user
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// The company name of the user
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// The mobile number of the user
        /// </summary>
        public string MobileNumber { get; set; }
        /// <summary>
        /// The date of birth of the user
        /// </summary>
        public DateTime? DateOfBirth { get; set; }
        /// <summary>
        /// The referrer of the user. Ignored for POST requests.
        /// </summary>
        public string Referrer { get; set; }
        /// <summary>
        /// true if user has subscribed to newsletters. false otherwise.
        /// </summary>
        public bool NewslettersEnabled { get; set; }

        /// <summary>
        /// The timezone id of the user
        /// </summary>
        public string TimeZoneId { get; set; }

        /// <summary>
        /// Specifies if user can change the profile picture
        /// </summary>
        public bool CanChangeProfilePicture { get; set; }

        /// <summary>
        /// The url of the user's profile picture
        /// </summary>
        public string ProfilePictureUrl { get; set; }

        /// <summary>
        /// The points earned by user as reputation
        /// </summary>
        public int Points { get; set; }
        /// <summary>
        /// Specifies if the user is or 'has applied to be' an affiliate
        /// </summary>
        public bool IsAffiliate { get; set; }
        /// <summary>
        /// Specifies if the affiliate account is active
        /// </summary>
        public bool AffiliateActive { get; set; }
        /// <summary>
        /// The date of activation of affiliate account
        /// </summary>
        public DateTime? AffiliateFirstActivationDate { get; set; }

        public void SetupValidationRules(ModelValidator<UserModel> v)
        {
            v.RuleFor(x => x.Email).NotEmpty().EmailAddress();
            v.RuleFor(x => x.FirstName).NotEmpty();
            v.RuleFor(x => x.LastName).NotEmpty();
        }
    }
}