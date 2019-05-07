using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DotLiquid;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace EvenCart.Infrastructure.Swagger
{
    public class SwaggerCommonFilter : IParameterFilter,IDocumentFilter
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
            if (IsDotLiquidType(context.PropertyInfo.DeclaringType) || context.PropertyInfo.GetGetMethod().IsVirtual)
            {
                parameter.Name = ExcludeKey + parameter.Name;
                return;
            }
            var name = context.PropertyInfo.Name == "Consents";
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
    }
}