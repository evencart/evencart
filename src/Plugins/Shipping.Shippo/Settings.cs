using EvenCart.Core.Config;

namespace Shipping.Shippo
{
    public class Settings : ISettingGroup
    {
        public bool DebugMode { get; set; }

        public string LiveApiKey { get; set; }

        public string TestApiKey { get; set; }
    }
}