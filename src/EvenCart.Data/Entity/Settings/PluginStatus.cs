using System;
using System.Collections.Generic;

namespace EvenCart.Data.Entity.Settings
{
    public class PluginStatus
    {
        public string PluginSystemName { get; set; }

        public bool Installed { get; set; }

        [Obsolete("Use ActiveStoreIds instead")]
        public bool Active { get; set; }

        public IList<int> ActiveStoreIds { get; set; }
    }
}