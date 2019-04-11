using System.Collections.Generic;
using EvenCart.Data.Enum;
using EvenCart.Infrastructure.Mvc.Models;

namespace EvenCart.Models.Products
{
    public class ProductAttributeModel : FoundationModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsRequired { get; set; }

        public IList<ProductAttributeValueModel> AvailableValues { get; set; }

        public IList<ProductAttributeValueModel> SelectedValues { get; set; }

        public InputFieldType InputFieldType { get; set; }
    }
}