﻿#region License
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
using Genesis.Infrastructure.Mvc.Models;

namespace EvenCart.Areas.Administration.Models.Reports
{
    public class StockReportModel : GenesisModel
    {
        public string ProductName { get; set; }

        public int ProductId { get; set; }

        public int StockQuantity { get; set; }

        public int ReservedQuantity { get; set; }

        public bool Published { get; set; }

        public bool HasVariants { get; set; }

        public IList<VariantStockReportModel> Variants { get; set; }

        public class VariantStockReportModel : GenesisModel
        {
            public string AttributeText { get; set; }

            public int StockQuantity { get; set; }

            public int ReservedQuantity { get; set; }
        }
    }
}