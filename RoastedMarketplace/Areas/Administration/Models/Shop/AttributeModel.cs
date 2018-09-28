using RoastedMarketplace.Data.Enum;
using RoastedMarketplace.Infrastructure.Mvc.Models;

namespace RoastedMarketplace.Areas.Administration.Models.Shop
{
    public class AttributeModel : FoundationModel
    {
        public int ProductAttributeId { get; set; }

        public int ProductAttributeValueId { get; set; }

        public string Name { get; set; }

        public string Value { get; set; }

        public InputFieldType InputFieldType { get; set; }

        public string NameLabel { get; set; }

        public string ValueLabel { get; set; }

        public int DisplayOrder { get; set; }

        public bool IsRequired { get; set; }
    }
}