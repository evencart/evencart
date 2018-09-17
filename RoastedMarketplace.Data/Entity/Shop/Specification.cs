using System.Collections.Generic;
using RoastedMarketplace.Core.Data;

namespace RoastedMarketplace.Data.Entity.Shop
{
    public class Specification : FoundationEntity
    {
        public string Name { get; set; }

        public int DisplayOrder { get; set; }

        public virtual IList<SpecificationValue> SpecificationValues { get; set; }
    }
}