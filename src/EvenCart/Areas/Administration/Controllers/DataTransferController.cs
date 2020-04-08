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

using System;
using System.Globalization;
using System.Threading.Tasks;
using EvenCart.Areas.Administration.Extensions;
using EvenCart.Areas.Administration.Models.DataTransfer;
using EvenCart.Core.Infrastructure;
using EvenCart.Data.Entity.Shop;
using EvenCart.Infrastructure.DataTransfer;
using EvenCart.Infrastructure.Mvc;
using EvenCart.Infrastructure.Mvc.Attributes;
using EvenCart.Infrastructure.Routing;
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

        [HttpGet("export", Name = AdminRouteNames.DataTransferExport)]
        [ValidateModelState(ModelType = typeof(ExportRequestModel))]
        public IActionResult Export(ExportRequestModel exportRequest)
        {
            IDataTransferProvider dataTransferProvider;
            dataTransferProvider = DependencyResolver.Resolve<IDataTransferProvider>(exportRequest.Output == "json" ? typeof(JsonProvider).FullName : typeof(ExcelProvider).FullName);

            switch (exportRequest.EntityName)
            {
                case nameof(Product):
                    var products = _productService.GetProducts(true, true, true, true, true, true);
                    var chunk = dataTransferProvider.GetTransferChunks(products);
                    var fileName = $"products-{DateTime.UtcNow.Date.ToString(CultureInfo.InvariantCulture)}.xlsx";
                    return File(chunk.Bytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);

            }

            return BadRequest();
        }

        [DualGet("import", Name = AdminRouteNames.DataTransferImport)]
        public IActionResult ImportEditor()
        {
            return R.Success.Result;
        }

        [DualPost("import", Name = AdminRouteNames.DataTransferImport, OnlyApi = true)]
        [ValidateModelState(ModelType = typeof(ImportRequestModel))]
        public async Task<IActionResult> Import(ImportRequestModel importRequest)
        {

            IDataTransferProvider dataTransferProvider;
            dataTransferProvider = DependencyResolver.Resolve<IDataTransferProvider>(importRequest.Input == "json" ? typeof(JsonProvider).FullName : typeof(ExcelProvider).FullName);
            var dataChunk = new DataTransferChunk()
            {
                Bytes = await importRequest.ImportFile.GetBytesAsync()
            };
            switch (importRequest.EntityName)
            {
                case nameof(Product):
                    var products = dataTransferProvider.GetProducts(dataChunk);
                    break;

            }
            return BadRequest();
        }
    }
}