// #region Author Information
// // FoundationExtraFieldModel.cs
// // 
// // (c) Apexol Technologies. All Rights Reserved.
// // 
// #endregion

using System.Collections.Generic;

namespace RoastedMarketplace.Infrastructure.Mvc.Models
{
    public abstract class FoundationExtraFieldModel : FoundationModel
    {
        public IList<ExtraFieldItemModel> SubmittedExtraFields { get; set; }
    }
}