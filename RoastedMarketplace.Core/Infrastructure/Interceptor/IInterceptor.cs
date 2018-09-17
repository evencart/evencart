#region Author Information
// IInterceptor.cs
// 
// Author 2016 Apexol Technologies. All Rights Reserved.
// 
// Created On: 01 07 2016 08:12 PM
// Last Modified: 01 07 2016 08:12 PM
#endregion

using System;
using System.Collections.Generic;

namespace RoastedMarketplace.Core.Infrastructure.Interceptor
{
    public interface IInterceptor
    {
        void SetupInterceptors();
    }
}