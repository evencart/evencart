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

using System.Collections.Generic;

namespace EvenCart.Core.Infrastructure.Interceptor
{
    public class InterceptorAction
    {
        public string InterceptorName { get; set; }

        public int Priority { get; set; }

        public IList<string> InterceptorLocations { get; set; }

        public InterceptorFunc Action { get; set; }

        public string Error { get; set; }
    }

    public delegate bool InterceptorFunc(params object[] parameters);
}