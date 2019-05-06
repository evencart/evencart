using EvenCart.Core.Data;

namespace EvenCart.Data.Entity.Users
{
    public partial class Capability : FoundationEntity
    {
        public string Name { get; set; }

        public bool IsActive { get; set; }
    }
}