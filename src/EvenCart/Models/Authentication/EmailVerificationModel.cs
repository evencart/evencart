using EvenCart.Infrastructure.Mvc.Models;
using EvenCart.Infrastructure.Mvc.Validator;
using FluentValidation;

namespace EvenCart.Models.Authentication
{
    /// <summary>
    /// Represents an email verification object
    /// </summary>
    public class EmailVerificationModel : FoundationModel, IRequiresValidations<EmailVerificationModel>
    {
        /// <summary>
        /// The email of the user
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// The code for verification
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// Set to true if a token should be returned upon successful login
        /// </summary>
        public bool Token { get; set; }

        public void SetupValidationRules(ModelValidator<EmailVerificationModel> v)
        {
            v.RuleFor(x => x.Code).NotEmpty();
            v.RuleFor(x => x.Email).EmailAddress().NotEmpty();
        }
    }
}