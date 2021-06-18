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
using System.Linq;
using System.Threading.Tasks;
using EvenCart.Areas.Administration.Extensions;
using EvenCart.Areas.Administration.Models.DataTransfer;
using EvenCart.Data.Entity.Shop;
using Genesis;
using Genesis.Helpers;
using Genesis.Infrastructure.Mvc;
using Genesis.Infrastructure.Mvc.Attributes;
using Genesis.Modules.DataTransfer;
using Genesis.Modules.Users;
using Genesis.Routing;
using Microsoft.AspNetCore.Mvc;

namespace EvenCart.Areas.Administration.Controllers
{
    /// <summary>
    /// Provides import/export of site data
    /// </summary>
    public class DataTransferController : GenesisAdminController
    {
        private static string[] Exportables = new[] {nameof(Product), nameof(User), nameof(Category)};

        private readonly IDataTransferManager _dataTransferManager;
        public DataTransferController(IDataTransferManager dataTransferManager)
        {
            _dataTransferManager = dataTransferManager;
        }

        /// <summary>
        /// Renders export editor page
        /// </summary>
        /// <param name="entityName">The name of entity to export</param>
        /// <param name="success">Specifies if the last operation was successful</param>
        /// <param name="importCount">The number of successfully inserted entities.</param>
        /// <response code="200">The export editor page with entityName</response>
        [HttpGet("", Name = AdminRouteNames.DataTransfer)]
        public IActionResult DataTransferEditor(string entityName, bool success, int importedCount)
        {
            var availableExportableEntities = SelectListHelper.GetSelectItemList(Exportables, entityName);
            return R.Success
                .With("availableExportableEntities", availableExportableEntities)
                .With("entityName", entityName)
                .With("importedCount", importedCount)
                .With("importSuccess", success).Result;
        }

        /// <summary>
        /// Exports the requested data in the output format
        /// </summary>
        /// <param name="exportRequest"></param>
        /// <response code="200">The data file in the output format</response>
        [HttpPost("export", Name = AdminRouteNames.DataTransferExport)]
        [ValidateModelState(ModelType = typeof(ExportRequestModel))]
        public IActionResult ExportEditor(ExportRequestModel exportRequest)
        {
            IDataTransferProvider dataTransferProvider = null;
            var outputType = "";
            var fileName = $"{exportRequest.EntityName}-{DateTime.UtcNow.Date.ToString(CultureInfo.InvariantCulture)}";
            switch (exportRequest.Output)
            {
                case "json":
                    dataTransferProvider =
                        D.Resolve<IDataTransferProvider>(typeof(JsonProvider).FullName);
                    outputType = "application/json";
                    fileName += ".json";
                    break;
                case "excel":
                    dataTransferProvider =
                        D.Resolve<IDataTransferProvider>(typeof(ExcelProvider).FullName);
                    outputType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    fileName += ".xlsx";
                    break;
                default:
                    return BadRequest();
            }

            DataTransferChunk chunk = null;
            switch (exportRequest.EntityName)
            {
                case nameof(Product):
                    chunk = _dataTransferManager.Export<Product>((IDataTransferProvider<Product>) dataTransferProvider);
                    return File(chunk.Bytes, outputType, fileName);
                case nameof(Category):
                    chunk = _dataTransferManager.Export<Category>((IDataTransferProvider<Category>)dataTransferProvider);
                    return File(chunk.Bytes, outputType, fileName);
                case nameof(User):
                    chunk = _dataTransferManager.Export<User>((IDataTransferProvider<User>)dataTransferProvider);
                    return File(chunk.Bytes, outputType, fileName);
            }

            return BadRequest();
        }

        /// <summary>
        /// Parses and Imports a data file
        /// </summary>
        /// <param name="importRequest"></param>
        /// <returns></returns>
        [HttpPost("import", Name = AdminRouteNames.DataTransferImport)]
        [ValidateModelState(ModelType = typeof(ImportRequestModel))]
        public async Task<IActionResult> Import(ImportRequestModel importRequest)
        {
            if (!Exportables.Contains(importRequest.EntityName))
                return NotFound();

            var dataTransferProvider = D.Resolve<IDataTransferProvider>(importRequest.Input == "json" ? typeof(JsonProvider).FullName : typeof(ExcelProvider).FullName);
            var dataChunk = new DataTransferChunk()
            {
                Bytes = await importRequest.ImportFile.GetBytesAsync()
            };
            var count = 0;
            switch (importRequest.EntityName)
            {
                case nameof(Product):
                    count = _dataTransferManager.Import<Product>(dataChunk, (IDataTransferProvider<Product>) dataTransferProvider);
                    return RedirectToRoute(AdminRouteNames.DataTransfer,
                        new {entityName = importRequest.EntityName, success = true, importedCount = count});
                case nameof(User):
                    count = _dataTransferManager.Import<User>(dataChunk, (IDataTransferProvider<User>) dataTransferProvider);
                    return RedirectToRoute(AdminRouteNames.DataTransfer,
                        new { entityName = importRequest.EntityName, success = true, importedCount = count });
                case nameof(Category):
                    count = _dataTransferManager.Import<Category>(dataChunk, (IDataTransferProvider<Category>) dataTransferProvider);
                    return RedirectToRoute(AdminRouteNames.DataTransfer,
                        new { entityName = importRequest.EntityName, success = true, importedCount = count });
            }
            return BadRequest();
        }
    }
}