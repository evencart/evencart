using EvenCart.Core.Config;

namespace EvenCart.Data.Entity.Settings
{
    public class UrlSettings : ISettingGroup
    {
        public string ProductUrlTemplate { get; set; }

        public string CategoryUrlTemplate { get; set; }

        public string ContentPageUrlTemplate { get; set; }
    }
}
