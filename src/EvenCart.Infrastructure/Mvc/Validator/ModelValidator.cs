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