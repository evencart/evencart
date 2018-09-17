using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;

namespace RoastedMarketplace.Infrastructure.ViewEngines
{
    public class RoastedLiquidView : IView
    {
        private readonly IViewAccountant _viewAccountant;
        public RoastedLiquidView(string viewName, IViewAccountant viewAccountant)
        {
            Path = viewName;
            _viewAccountant = viewAccountant;
        }

        public async Task RenderAsync(ViewContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            var view =_viewAccountant.RenderView(context);
            await context.Writer.WriteAsync(view);
        }

        public string Path { get; }
    }
}