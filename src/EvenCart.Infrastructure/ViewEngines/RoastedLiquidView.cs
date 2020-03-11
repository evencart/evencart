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