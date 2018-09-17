// #region Author Information
// // FeedbackSettings.cs
// // 
// // (c) 2017 Apexol Technologies. All Rights Reserved.
// // 
// #endregion

using RoastedMarketplace.Core.Config;
using RoastedMarketplace.Data.Enum;

namespace RoastedMarketplace.Data.Entity.Settings
{
    public class FeedbackSettings : ISettingGroup
    {
        public bool AllowAnonymousFeedback { get; set; }

        public FeedbackModerationType FeedbackModerationType { get; set; }
    }
}