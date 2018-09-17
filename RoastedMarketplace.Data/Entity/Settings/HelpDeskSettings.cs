#region Author Information
// HelpDeskSettings.cs
// 
// (c) 2016 Apexol Technologies. All Rights Reserved.
// 
#endregion

using RoastedMarketplace.Core.Config;

namespace RoastedMarketplace.Data.Entity.Settings
{
    public class HelpDeskSettings : ISettingGroup
    {
        public int TicketsPerPageUser { get; set; }

        public int TicketsPerPageAdministration { get; set; }

        public int RepliesPerPageUser { get; set; }

        public int RepliesPerPageAdministration { get; set; }

    }
}