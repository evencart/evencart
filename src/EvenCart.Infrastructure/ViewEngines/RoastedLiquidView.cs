using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;

namespace EvenCart.Infrastructure.ViewEngines
{
    public class RoastedLiquidView : IView
    {
        private readonly IViewAccountant _viewAccountant;
        private readonly string _requestedPath;
        public RoastedLiquidView(string viewName, string requestedPath, IViewAccountant viewAccountant)
        {
            Path = viewName;
            _viewAccountant = viewAccountant;
            _requestedPath = requestedPath;
        }

        public async Task RenderAsync(ViewContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));
            //preserve view path
            context.TempData.Add("requested_path", _requestedPath);
            var view =_viewAccountant.RenderView(context);
            await context.Writer.WriteAsync(view);
        }

        public string Path { get; }
    }
}