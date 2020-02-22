using System;
using FluentValidation;
using FluentValidation.Results;

namespace EvenCart.Infrastructure.Mvc.Validator
{
    public class ModelValidator<T> : AbstractValidator<T>
    {
        public static ValidationResult ValidateEntity(T entity)
        {
            if (!(entity is IRequiresValidations<T>))
            {
                throw new Exception("Can not validate an entity without IRequiresValidations implementation");
            }

            var validator = new ModelValidator<T>();
            var entityAsRequiresValidations = entity as IRequiresValidations<T>;

            //setup the validation rules
            entityAsRequiresValidations.SetupValidationRules(validator);
            //perform the validation
            return validator.Validate(entity);
        }
    }
}