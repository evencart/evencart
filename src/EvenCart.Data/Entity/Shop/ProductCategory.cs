﻿using EvenCart.Core.Data;

namespace EvenCart.Data.Entity.Shop
{
    public class ProductCategory : FoundationEntity
    {
        public int ProductId { get; set; }

        public int CategoryId { get; set; }

        public int DisplayOrder { get; set; }
    }
}