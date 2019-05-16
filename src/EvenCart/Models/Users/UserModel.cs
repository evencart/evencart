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

        public void SetupValidationRules(ModelValidator<UserModel> v)
        {
            v.RuleFor(x => x.Email).NotEmpty().EmailAddress();
        }
    }
}