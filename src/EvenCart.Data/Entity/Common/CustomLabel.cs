using EvenCart.Core.Data;

namespace EvenCart.Data.Entity.Common
{
    public class CustomLabel : FoundationEntity
    {
        public string Text { get; set; }

        public string Type { get; set; }

        public int DisplayOrder { get; set; }
    }
}