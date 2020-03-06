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
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace EvenCart.Swagger
{
    public class SwaggerCommonFilter : IParameterFilter,IDocumentFilter, IOperationFilter, ISchemaFilter
    {
        private bool IsDotLiquidType(Type type)
        {
            return type.FullName.StartsWith("DotLiquid.");
        }

        private const string ExcludeKey = "__EXCLUDE__";
        public void Apply(IParameter parameter, ParameterFilterContext context)
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
                parameter.Extensions.Add("targetType", genericType.FullName);
            }
            
         }

        public void Apply(SwaggerDocument swaggerDoc, DocumentFilterContext context)
        {
            ////remove all the definitions which are not specific to evencart
            //foreach (var definition in swaggerDoc.Definitions.Where(x => !x.Key.StartsWith("EvenCart.")).ToList())
            //{
            //    swaggerDoc.Definitions.Remove(definition);
            //}

            foreach (var (pathKey, pathItem) in swaggerDoc.Paths)
            {
                if (pathItem.Get != null)
                    pathItem.Get.Parameters =
                        pathItem.Get.Parameters.Where(x => !x.Name.StartsWith(ExcludeKey)).ToList();
                if (pathItem.Post != null)
                    pathItem.Post.Parameters =
                        pathItem.Post.Parameters.Where(x => !x.Name.StartsWith(ExcludeKey)).ToList();
                if (pathItem.Put != null)
                    pathItem.Put.Parameters =
                        pathItem.Put.Parameters.Where(x => !x.Name.StartsWith(ExcludeKey)).ToList();
                if (pathItem.Patch != null)
                    pathItem.Patch.Parameters =
                        pathItem.Patch.Parameters.Where(x => !x.Name.StartsWith(ExcludeKey)).ToList();
                if (pathItem.Delete != null)
                    pathItem.Delete.Parameters =
                        pathItem.Delete.Parameters.Where(x => !x.Name.StartsWith(ExcludeKey)).ToList();
            }
            
        }

        public void Apply(Operation operation, OperationFilterContext context)
        {
            foreach (var response in operation.Responses)
            {
                var description = response.Value.Description;
                var crefTypes = SwaggerHelper.GetCrefTypes(description);
                if (!crefTypes.Any())
                    continue;
                var types = TypeFinder.GetByNames(crefTypes);
                foreach (var type in types)
                    context.SchemaRegistry.GetOrRegister(type);
            }
           
            operation.Produces.Add("application/json");
        }

        public void Apply(Schema schema, SchemaFilterContext context)
        {
            
        }
    }
}