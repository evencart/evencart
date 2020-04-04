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
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using EvenCart.Core.Extensions;
using EvenCart.Data.Entity.Shop;
using EvenCart.Data.Entity.Users;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace EvenCart.Infrastructure.DataTransfer
{
    public class ExcelProvider : IDataTransferProvider
    {
        public DataTransferChunk GetTransferChunks(IList<Product> products)
        {
            IWorkbook workbook = new XSSFWorkbook();
            FillSheet(workbook, typeof(Product), products);
            return GetChunk(workbook);
        }

        public DataTransferChunk GetTransferChunks(IList<Category> categories)
        {
            throw new System.NotImplementedException();
        }

        public DataTransferChunk GetTransferChunks(IList<User> users)
        {
            throw new System.NotImplementedException();
        }

        public IList<Product> GetProducts(DataTransferChunk chunk)
        {
            throw new System.NotImplementedException();
        }

        public IList<Category> GetCategories(DataTransferChunk chunk)
        {
            throw new System.NotImplementedException();
        }

        public IList<User> GetUsers(DataTransferChunk chunk)
        {
            throw new System.NotImplementedException();
        }

        private ISheet FillSheet(IWorkbook workbook, Type type, IEnumerable entities)
        {
            //create a product work sheet
            var sheet = workbook.GetSheet(type.Name) ?? workbook.CreateSheet(type.Name);
            if (entities == null)
                return sheet;
            //get db properties
            var dbProperties = type.GetProperties().Where(x => !x.GetMethod.IsVirtual).ToArray();
            //and the virtual properties
            var virtualProperties = type.GetProperties().Where(x => x.GetMethod.IsVirtual && !x.PropertyType.IsPrimitive()).ToArray();

            //top line should be headings
            var headingRow = sheet.CreateRow(0);
            for (var index = 0; index < dbProperties.Length; index++)
            {
                var headingCell = headingRow.CreateCell(index);
                headingCell.SetCellValue(dbProperties[index].Name);
            }

            var enumerable = entities as object[] ?? entities.Cast<object>().ToArray();
            var enumerator = enumerable.GetEnumerator();
            var itemIndex = 1;
            while (enumerator.MoveNext())
            {
                var entity = enumerator.Current;
                CreateRow(entity, sheet, itemIndex++, dbProperties);

                foreach (var vp in virtualProperties)
                {
                    var propertyType = vp.PropertyType;
                    if (propertyType != typeof(string) && typeof(IEnumerable).IsAssignableFrom(vp.PropertyType))
                    {
                        var vpObject = (IEnumerable) vp.GetValue(entity);
                        FillSheet(workbook, propertyType.GetGenericArguments().First(), vpObject);
                    }
                    else
                    {
                        var vpObject = vp.GetValue(entity);
                        if (vpObject != null)
                            FillSheet(workbook, propertyType, new[] {vpObject});
                    }
                }
            }
            return sheet;
        }

        private IRow CreateRow(object entity, ISheet sheet, int index, PropertyInfo[] dbProperties)
        {
            var newRow = sheet.CreateRow(index + 1);
            for (var cellIndex = 0; cellIndex < dbProperties.Length; cellIndex++)
            {
                var rowCell = newRow.CreateCell(cellIndex);
                rowCell.SetCellValue(dbProperties[cellIndex].GetValue(entity)?.ToString());
            }
            return newRow;
        }

        private DataTransferChunk GetChunk(IWorkbook workbook)
        {
            //write the stream to memory
            using (var stream = new MemoryStream())
            {
                workbook.Write(stream);
                var chunk = new DataTransferChunk()
                {
                    Bytes = stream.ToArray()
                };
                return chunk;
            }
        } 
    }
}