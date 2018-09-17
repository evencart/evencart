#region Author Information
// InterceptorException.cs
// 
// Author 2016 Apexol Technologies. All Rights Reserved.
// 
// Created On: 04 07 2016 06:17 PM
// Last Modified: 04 07 2016 06:17 PM
#endregion

using Microsoft.AspNetCore.Mvc;

namespace RoastedMarketplace.Core.Exception
{
    public class InterceptorException : RoastedMarketplaceException
    {
        public IActionResult OriginalResult { get; set; }
    }
}