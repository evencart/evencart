using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace RoastedMarketplace.Infrastructure.Mvc.Models
{
    public class FoundationModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (context.Metadata.ModelType == typeof(FoundationModel))
            {
                return new BinderTypeModelBinder(typeof(FoundationModelBinder));
            }

            return null;
        }
    }
}