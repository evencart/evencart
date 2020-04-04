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

using EvenCart.Infrastructure.DataTransfer;
using EvenCart.Infrastructure.Mvc;
using EvenCart.Services.Products;
using Microsoft.AspNetCore.Mvc;

namespace EvenCart.Areas.Administration.Controllers
{
    public class DataTransferController : FoundationAdminController
    {
        private readonly IProductService _productService;
        public DataTransferController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("products")]
        public IActionResult ExportProducts()
        {
            var excelProvider = new ExcelProvider();
            var products = _productService.GetProducts(out _, out _, out _, out _, out _, out _);
            var chunk = excelProvider.GetTransferChunks(products);
            return File(chunk.Bytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "products.xlsx");
        }
    }
}