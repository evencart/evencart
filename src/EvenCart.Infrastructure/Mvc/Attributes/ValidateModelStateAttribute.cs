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
using System.Collections.Generic;
using System.Linq;
using EvenCart.Infrastructure.Mvc.Validator;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace EvenCart.Infrastructure.Mvc.Attributes
{
    /// <summary>
    /// Validates the model of the requested action and returns a 400 if it's an invalid model 
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ValidateModelStateAttribute : ActionFilterAttribute
    {
        public Type ModelType { get; set; }

        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            if (!actionContext.ModelState.IsValid)
            {
                actionContext.Result = new OkObjectResult(new { success = false, errors = ParseModelState(actionContext.ModelState) });
            }
            else
            {
                if (ModelType == null)
                    return;
                //let's perform some fluent validation
                var typeObject = actionContext.ActionArguments.Select(x => x.Value).FirstOrDefault(x => ModelType.IsInstanceOfType(x));
                if (typeObject == null)
                    return; //nothing to validate

                //reflection based validation
                var classType = typeof(ModelValidator<>);
                var boundClassType = classType.MakeGenericType(ModelType);
                var methodInfo = boundClassType.GetMethod("ValidateEntity");
                //var genericMethod = methodInfo.MakeGenericMethod(ModelType);
                //invoke this method
                var validationResult = (ValidationResult)methodInfo.Invoke(null, new[] { typeObject });
                if (validationResult == null || !validationResult.Errors.Any())
                    return;//no errors

                foreach (var validationFailure in validationResult.Errors)
                {
                    actionContext.ModelState.AddModelError(validationFailure.PropertyName, validationFailure.ErrorMessage);
                }

                if (!actionContext.ModelState.IsValid)
                {
                    actionContext.Result = new OkObjectResult(new { success = false, errors = ParseModelState(actionContext.ModelState) });
                }
            }
        }

        private List<string> ParseModelState(ModelStateDictionary modelState)
        {
            var errors = new List<string>();
            foreach (var state in modelState)
            {
                foreach (var error in state.Value.Errors)
                {
                    errors.Add(error.ErrorMessage);
                }
            }
            return errors;
        }
    }
}