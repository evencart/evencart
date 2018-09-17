using RoastedMarketplace.Core.Config;

namespace RoastedMarketplace.Data.Entity.Settings
{
    public class ConversationSettings : ISettingGroup
    {
        public bool HideAgentName { get; set; }

        public string DefaultAgentName { get; set; }

        public bool HideAgentImage { get; set; }
    }
}