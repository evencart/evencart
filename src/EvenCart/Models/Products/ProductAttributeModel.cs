using System.Collections.Generic;
using EvenCart.Data.Enum;
using EvenCart.Infrastructure.Mvc.Models;

namespace EvenCart.Models.Products
{
    public class ProductAttributeModel : FoundationEntityModel
    {
        /// <summary>
        /// The name of attribute
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Is the attribute required
        /// </summary>
        public bool IsRequired { get; set; }

        /// <summary>
        /// A collection of <see cref="ProductAttributeValueModel">available value</see> objects. Ignored for POST requests.
        /// </summary>
        public IList<ProductAttributeValueModel> AvailableValues { get; set; }

        /// <summary>
        /// A collection of <see cref="ProductAttributeValueModel">selected value</see> objects.
        /// </summary>
        public IList<ProductAttributeValueModel> SelectedValues { get; set; }
        /// <summary>
        /// The type of input field
        /// </summary>
        public InputFieldType InputFieldType { get; set; }
    }
}