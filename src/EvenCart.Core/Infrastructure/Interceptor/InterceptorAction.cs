#region Author Information
// InterceptorAction.cs
// 
// Author 2016 Apexol Technologies. All Rights Reserved.
// 
// Created On: 01 07 2016 08:30 PM
// Last Modified: 01 07 2016 08:30 PM
#endregion

using System;
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