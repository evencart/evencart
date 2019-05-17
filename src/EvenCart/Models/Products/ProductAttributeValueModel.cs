using EvenCart.Infrastructure.Mvc.Models;

namespace EvenCart.Models.Products
{
    public class ProductAttributeValueModel : FoundationModel
    {
        /// <summary>
        /// The name of attribute value
        /// </summary>
        public string Name { get; set; }
    }
}