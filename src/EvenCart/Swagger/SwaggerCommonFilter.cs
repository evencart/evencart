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
using System.Collections;
using System.Linq;
using EvenCart.Core.Infrastructure.Utils;
using EvenCart.Data.Extensions;
using FastExpressionCompiler.LightExpression;
using Microsoft.AspNetCore.JsonPatch.Operations;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using OperationType = Microsoft.OpenApi.Models.OperationType;

namespace EvenCart.Swagger
{
    public class SwaggerCommonFilter : IParameterFilter,IDocumentFilter, IOperationFilter
    {
        private bool IsDotLiquidType(Type type)
        {
            return type.FullName.StartsWith("DotLiquid.");
        }

        private const string ExcludeKey = "__EXCLUDE__";
        public void Apply(OpenApiParameter parameter, ParameterFilterContext context)
        {
            if (context.PropertyInfo == null)
                return;
            //we'll exclude dotliquid types and virtual properties from parameters
            if (IsDotLiquidType(context.PropertyInfo.DeclaringType) || context.PropertyInfo.GetGetMethod().IsVirtual || !context.PropertyInfo.CanWrite)
            {
                parameter.Name = ExcludeKey + parameter.Name;
                return;
            }
            if (typeof(IEnumerable).IsAssignableFrom(context.PropertyInfo.PropertyType) && context.PropertyInfo.PropertyType.IsGenericType)
            {
                var genericType = context.PropertyInfo.PropertyType.GenericTypeArguments[0];
                //parameter.Extensions.Add("targetType", );
            }
         }

        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            ////remove all the definitions which are not specific to evencart
            //foreach (var definition in swaggerDoc.Definitions.Where(x => !x.Key.StartsWith("EvenCart.")).ToList())
            //{
            //    swaggerDoc.Definitions.Remove(definition);
            //}
            var operationTypes = Enum.GetValues(typeof(OperationType)).Cast<OperationType>().ToList();
            foreach (var (pathKey, pathItem) in swaggerDoc.Paths)
            {
                foreach (var operationType in operationTypes)
                {
                    if (pathItem.Operations.ContainsKey(operationType))
                        pathItem.Operations[operationType].Parameters =
                            pathItem.Operations[operationType].Parameters.Where(x => !x.Name.StartsWith(ExcludeKey)).ToList();
                }
            }
            
        }

        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            foreach (var response in operation.Responses)
            {
                var description = response.Value.Description;
                var crefTypes = SwaggerHelper.GetCrefTypes(description);
                if (!crefTypes.Any())
                    continue;
                var types = TypeFinder.GetByNames(crefTypes);
                foreach (var type in types)
                    context.SchemaGenerator.GenerateSchema(type, context.SchemaRepository);
            }
        }
    }
}