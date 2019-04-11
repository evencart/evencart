using System.Collections.Generic;
using EvenCart.Core.Data;

namespace EvenCart.Data.Entity.Users
{
    public class Role : FoundationEntity
    {
        public string Name { get; set; }

        public bool IsSystemRole { get; set; }

        public string SystemName { get; set; }

        public bool IsActive { get; set; }

        #region Virtual Properties
        public virtual IList<Capability> Capabilities { get; set; }
        #endregion

    }
}