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

using Genesis.Bundle;
using Genesis.Infrastructure;
using Genesis.Infrastructure.IO;
using Genesis.Infrastructure.Theming;
using Genesis.ViewEngines;
using Genesis.ViewEngines.GlobalObjects;

namespace EvenCart.Genesis.ViewEngines
{
    public class EvenCartViewAccountant : ViewAccountant
    {
        public EvenCartViewAccountant(ILocalFileProvider localFileProvider, IThemeProvider themeProvider, IMinifier minifier, IGenesisEngine genesisEngine) : base(localFileProvider, themeProvider, minifier, genesisEngine)
        {
            GlobalObject.RegisterObject<StoreObject>(GlobalObjectKeys.Store);
            GlobalObject.RegisterObject<CartObject>(GlobalObjectKeys.Cart);
            GlobalObject.RegisterObject<NavigationObject>(GlobalObjectKeys.Navigation);
        }
    }
}