using System;
using EvenCart.Infrastructure.Mvc.Models;
using EvenCart.Infrastructure.Mvc.Validator;
using FluentValidation;

namespace EvenCart.Areas.Administration.Models.Users
{
    public class UserPointModel : FoundationEntityModel, IRequiresValidations<UserPointModel>
    {
        public int UserId { get; set; }

        public int Points { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Reason { get; set; }

        public int ActivatorUserId { get; set; }

        public UserMiniModel ActivatorUser { get; set; }

        public void SetupValidationRules(ModelValidator<UserPointModel> v)
        {
            v.RuleFor(x => x.UserId).GreaterThan(0);
            v.RuleFor(x => x.Points).NotEqual(0).When(x => x.Id < 1);
        }
    }
}