#region License
// Copyright (c) Sojatia Infocrafts Private Limited.
// The following code is part of EvenCart eCommerce Software (https://evencart.co) Dual Licensed under the terms of
// 
// 1. GNU GPLv3 with additional terms (available to read at https://evencart.co/license)
// 2. EvenCart Proprietary License (available to read at https://evencart.co/license/commercial-license).
// 
// You can select one of the above two licenses according to your requirements. The usage of this code is
// subject to the terms of the license chosen by you.
#endregion

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