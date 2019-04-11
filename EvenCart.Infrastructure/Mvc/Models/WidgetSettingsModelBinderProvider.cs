using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace EvenCart.Infrastructure.Mvc.Models
{
    public class WidgetSettingsModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (context.Metadata.ModelType == typeof(WidgetSettingsModel))
            {
                return new BinderTypeModelBinder(typeof(WidgetSettingsModelBinder));
            }

            return null;
        }
    }
}