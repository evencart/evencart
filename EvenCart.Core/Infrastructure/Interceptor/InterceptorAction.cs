#region Author Information
// InterceptorAction.cs
// 
// Author 2016 Apexol Technologies. All Rights Reserved.
// 
// Created On: 01 07 2016 08:30 PM
// Last Modified: 01 07 2016 08:30 PM
#endregion

using System;

namespace EvenCart.Core.Infrastructure.Interceptor
{
    public class InterceptorAction
    {
        public string InterceptorName { get; set; }

        public int Priority { get; set; }

        public string InterceptorLocationName { get; set; }

        public Func<object[], bool> Action { get; set; }

        public string Error { get; set; }
    }
}