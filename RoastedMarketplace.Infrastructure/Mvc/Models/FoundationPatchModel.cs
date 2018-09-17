#region Author Information
// IPatchModel.cs
// 
// (c) 2016 Apexol Technologies. All Rights Reserved.
// 
#endregion

using System;
using System.Collections.Generic;
using RoastedMarketplace.Core.DataStructures;

namespace RoastedMarketplace.Infrastructure.Mvc.Models
{
    public abstract class FoundationPatchModel : FoundationModel
    {
        /// <summary>
        /// Stores the patched fields with values
        /// </summary>
        public abstract Dictionary<string, Pair<object, bool>> PatchFields { get; set; }
    }
}