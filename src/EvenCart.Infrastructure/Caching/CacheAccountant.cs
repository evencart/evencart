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

using EvenCart.Core.Caching;
using EvenCart.Infrastructure.Theming;
using EvenCart.Infrastructure.ViewEngines;
using EvenCart.Infrastructure.ViewEngines.Expanders;

namespace EvenCart.Infrastructure.Caching
{
    public class CacheAccountant : ICacheAccountant
    {
        private readonly ICacheProvider _cacheProvider;
        private readonly IThemeProvider _themeProvider;
        private readonly IViewAccountant _viewAccountant;
        public CacheAccountant(ICacheProvider cacheProvider, IThemeProvider themeProvider, IViewAccountant viewAccountant)
        {
            _cacheProvider = cacheProvider;
            _themeProvider = themeProvider;
            _viewAccountant = viewAccountant;
        }

        public void PurgeCache()
        {
            //clear the provider cache
            _cacheProvider.Clear();
            //clear the view cache
            ReadFile.PurgeCache();
            //clear the active theme template cache
            _themeProvider.ResetActiveTheme();
            //clear view location caches
            _viewAccountant.ClearCachedViews();
        }
    }
}