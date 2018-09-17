using RoastedMarketplace.Core.Data;

namespace RoastedMarketplace.Data.Entity.Settings
{
    public class Setting : FoundationEntity
    {
        public string GroupName { get; set; }

        public string Key { get; set; }

        public string Value { get; set; }
    }

}