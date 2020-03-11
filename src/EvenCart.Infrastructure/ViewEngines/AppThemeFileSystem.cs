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

using DotLiquid;
using DotLiquid.Exceptions;
using DotLiquid.FileSystems;

namespace EvenCart.Infrastructure.ViewEngines
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
            return _viewAccountant.RenderView(viewPath, templateName, null , context.Scopes[0]);
        }
    }
}