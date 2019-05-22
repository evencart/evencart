using EvenCart.Infrastructure.Mvc.Models;
using EvenCart.Infrastructure.Mvc.Validator;
using FluentValidation;

namespace EvenCart.Areas.Administration.Models.Users
{
    /// <summary>
    /// The parameters for invitation
    /// </summary>
    public class GenerateInvitationLinkModel : FoundationModel, IRequiresValidations<GenerateInvitationLinkModel>
    {
        /// <summary>
        /// The email address to which invite link needs to be sent
        /// </summary>
        public string Email { get; set; }

        public void SetupValidationRules(ModelValidator<GenerateInvitationLinkModel> v)
        {
            v.RuleFor(x => x.Email).EmailAddress();
        }
    }
}