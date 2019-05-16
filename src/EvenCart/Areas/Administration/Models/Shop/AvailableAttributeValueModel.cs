using EvenCart.Infrastructure.Mvc.Models;

namespace EvenCart.Areas.Administration.Models.Shop
{
    public class AvailableAttributeValueModel : FoundationEntityModel
    {
        /// <summary>
        /// A single value for the attribute
        /// </summary>
        public string Value { get; set; }
    }
}