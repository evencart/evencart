using System.Linq;
using DotLiquid;
using DotLiquid.Exceptions;
using DotLiquid.FileSystems;

namespace RoastedMarketplace.Infrastructure.ViewEngines
{
    public class AppThemeFileSystem : IFileSystem
    {
        private readonly IViewAccountant _viewAccountant;
        public AppThemeFileSystem(IViewAccountant viewAccountant)
        {
            _viewAccountant = viewAccountant;
        }

        public string ReadTemplateFile(Context context, string templateName)
        {
            templateName = templateName.Trim('"');
            var viewPath = _viewAccountant.GetThemeViewPath(templateName);
            if (string.IsNullOrEmpty(viewPath))
            {
                throw new FileSystemException(
                    $"The template '{templateName}' was not found. The following locations were searched:<br/>{string.Join("<br/>", _viewAccountant.GetSearchLocations())}");
            }
            return _viewAccountant.RenderView(viewPath, templateName, context.Scopes[0]);
        }
    }
}