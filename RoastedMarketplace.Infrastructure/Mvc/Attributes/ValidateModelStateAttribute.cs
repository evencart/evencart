#region Author Information
// ModelStateValidationFilterAttribute.cs
// 
// (c) 2016 Apexol Technologies. All Rights Reserved.
// 
#endregion

using System;
using System.Linq;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RoastedMarketplace.Infrastructure.Mvc.Validator;

namespace RoastedMarketplace.Infrastructure.Mvc.Attributes
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
                actionContext.Result = new BadRequestObjectResult(actionContext.ModelState);
            }
            else
            {
                if (ModelType == null)
                    return;
                //let's perform some fluent validation
                var typeObject = actionContext.ActionArguments.Select(x => x.Value).FirstOrDefault(x => x.GetType() == ModelType);
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
                    actionContext.Result = new BadRequestObjectResult(actionContext.ModelState);
                }
            }
        }
    }
}