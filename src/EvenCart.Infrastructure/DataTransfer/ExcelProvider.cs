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
using EvenCart.Data.Entity.MediaEntities;
using EvenCart.Data.Entity.Pages;
using EvenCart.Data.Entity.Shop;
using EvenCart.Data.Entity.Users;
using EvenCart.Data.Extensions;
using EvenCart.Services.Products;
using Npoi.Mapper;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace EvenCart.Infrastructure.DataTransfer
{
    public class ExcelProvider : IDataTransferProvider
    {
        const string DateFormat = "yyyy/MM/dd";
        public DataTransferChunk GetTransferChunks(IList<Product> products)
        {
            IWorkbook workbook = new XSSFWorkbook();
            FillSheet(workbook, typeof(Product), products);
            return GetChunk(workbook);
        }

        public DataTransferChunk GetTransferChunks(IList<Category> categories)
        {
            IWorkbook workbook = new XSSFWorkbook();
            FillSheet(workbook, typeof(Category), categories);
            return GetChunk(workbook);
        }

        public DataTransferChunk GetTransferChunks(IList<User> users)
        {
            IWorkbook workbook = new XSSFWorkbook();
            FillSheet(workbook, typeof(User), users);
            return GetChunk(workbook);
        }

        public IList<Product> GetProducts(DataTransferChunk chunk)
        {
            using (var memoryStream = new MemoryStream(chunk.Bytes))
            {
                IWorkbook workbook = new XSSFWorkbook(memoryStream);
                var mapper = new Mapper(workbook);
                
                var products = ReadSheet<Product>(mapper);

                var seoMetas = ReadSheet<SeoMeta>(mapper);

                var categories = ReadSheet<Category>(mapper);

                var productCategories = ReadSheet<ProductCategory>(mapper);

                var mediaItems = ReadSheet<Media>(mapper);

                var productMediaItems = ReadSheet<ProductMedia>(mapper);

                var availableAttributes = ReadSheet<AvailableAttribute>(mapper);

                var availableAttributeValues = ReadSheet<AvailableAttributeValue>(mapper);

                var specGroups = ReadSheet<ProductSpecificationGroup>(mapper);

                var productSpecifications = ReadSheet<ProductSpecification>(mapper);

                var productSpecificationValues = ReadSheet<ProductSpecificationValue>(mapper);

                var productVariants = ReadSheet<ProductVariant>(mapper);

                var productVendors = ReadSheet<ProductVendor>(mapper);

                var vendors = ReadSheet<Vendor>(mapper);

                var productAttributes = ReadSheet<ProductAttribute>(mapper);

                var productAttributeValues = ReadSheet<ProductAttributeValue>(mapper);

                var manufacturers = ReadSheet<Manufacturer>(mapper);

                var productVariantAttributes = ReadSheet<ProductVariantAttribute>(mapper);

                availableAttributes.ForEach(attribute =>
                    {
                        attribute.AvailableAttributeValues = availableAttributeValues?
                            .Where(x => x.AvailableAttributeId == attribute.Id).ToList();
                    });

                productSpecifications.ForEach(specification =>
                {
                    specification.AvailableAttribute = availableAttributes?
                        .FirstOrDefault(x => x.Id == specification.AvailableAttributeId);
                    specification.ProductSpecificationGroup =
                        specGroups.FirstOrDefault(x => x.Id == specification.ProductSpecificationGroupId);
                    specification.ProductSpecificationValues = productSpecificationValues
                        .Where(x => x.ProductSpecificationId == specification.Id).ToList();

                });

                productAttributes.ForEach(attribute =>
                {
                    attribute.AvailableAttribute = availableAttributes?
                        .FirstOrDefault(x => x.Id == attribute.AvailableAttributeId);
                  
                    attribute.ProductAttributeValues = productAttributeValues
                        .Where(x => x.ProductAttributeId == attribute.Id).ToList();
                    attribute.ProductAttributeValues.ForEach(value =>
                        {
                            value.AvailableAttributeValue =
                                availableAttributeValues.First(x => x.Id == value.AvailableAttributeValueId);
                        });

                });

                productVariants.ForEach(variant =>
                {
                    variant.ProductVariantAttributes = productVariantAttributes
                        .Where(x => x.ProductVariantId == variant.Id).ToList();
                    variant.ProductVariantAttributes.ForEach(attribute =>
                        {
                            attribute.ProductAttribute =
                                productAttributes.FirstOrDefault(x => x.Id == attribute.ProductAttributeId);
                            attribute.ProductAttributeValue =
                                productAttributeValues.FirstOrDefault(x => x.Id == attribute.ProductAttributeValueId);
                        });
                });

                categories.ForEach(category =>
                {
                    if (category.ParentId != 0)
                    {
                        category.Parent = categories.First(x => x.Id == category.ParentId);
                    }
                });
              
                products.ForEach(product =>
                    {
                        product.SeoMeta = seoMetas.FirstOrDefault(x =>
                            x.EntityId == product.Id && x.EntityName == nameof(Product));

                        product.Manufacturer = manufacturers.FirstOrDefault(x => x.Id == product.ManufacturerId);
                        if (product.Manufacturer != null)
                            product.Manufacturer.SeoMeta = seoMetas.FirstOrDefault(x =>
                                x.EntityId == product.Manufacturer.Id && x.EntityName == nameof(Manufacturer));
                        var pcIds = productCategories?.Where(x => x.ProductId == product.Id).Select(x => x.CategoryId);
                        if (pcIds != null)
                        {
                            product.Categories = categories?.Where(x => pcIds.Contains(x.Id)).ToList();
                        }

                        var pmIds = productMediaItems?.Where(x => x.ProductId == product.Id).Select(x => x.MediaId)
                            .ToList();
                        if (pmIds != null)
                        {
                            product.MediaItems = mediaItems?.Where(x => pmIds.Contains(x.Id)).ToList();
                        }

                        product.ProductSpecifications =
                            productSpecifications?.Where(x => x.ProductId == product.Id).ToList();

                        product.ProductAttributes = productAttributes?.Where(x => x.ProductId == product.Id).ToList();

                        var pvIds = productVendors?.Where(x => x.ProductId == product.Id).Select(x => x.VendorId).ToList();
                        if (pvIds != null)
                            product.Vendors = vendors?.Where(x => pvIds.Contains(x.Id)).ToList();

                        product.ProductVariants = productVariants?.Where(x => x.ProductId == product.Id).ToList();
                        
                    });
                return products;
            }
        }

        public IList<Category> GetCategories(DataTransferChunk chunk)
        {
            using (var memoryStream = new MemoryStream(chunk.Bytes))
            {
                IWorkbook workbook = new XSSFWorkbook(memoryStream);
                var mapper = new Mapper(workbook);
                var categories = ReadSheet<Category>(mapper);
                return categories;
            }
        }

        public IList<User> GetUsers(DataTransferChunk chunk)
        {
            using (var memoryStream = new MemoryStream(chunk.Bytes))
            {
                IWorkbook workbook = new XSSFWorkbook(memoryStream);
                var mapper = new Mapper(workbook);
                mapper.Format<User>(DateFormat, user => user.LastActivityDate);
                mapper.Format<User>(DateFormat, user => user.LastLoginDate);
                mapper.Format<User>(DateFormat, user => user.AffiliateFirstActivationDate);
                mapper.Format<User>(DateFormat, user => user.LastActivityDate);
                mapper.Format<User>(DateFormat, user => user.CreatedOn);
                mapper.Format<User>(DateFormat, user => user.UpdatedOn);
                mapper.Format<User>(DateFormat, user => user.FirstActivationDate);
                var users = ReadSheet<User>(mapper);
                var userRoles = ReadSheet<UserRole>(mapper);
                var roles = ReadSheet<Role>(mapper);

                users.ForEach(user =>
                {
                    user.UserRoles = userRoles.Where(x => x.UserId == user.Id).ToList();
                    var userRoleIds = user.UserRoles.Select(x => x.RoleId).ToList();
                    user.Roles = roles.Where(x => userRoleIds.Contains(x.Id)).ToList();
                    user.UserRoles.ForEach(userRole =>
                    {
                        userRole.User = user;
                        userRole.Role = user.Roles.FirstOrDefault(x => x.Id == userRole.Id);
                    });
                });
                return users;
            }
        }

        private readonly IList<object> writtenObjects = new List<object>();
        private ISheet FillSheet(IWorkbook workbook, Type type, IEnumerable entities)
        {
            if (entities == null)
                return null;
            //get db properties
            var dbProperties = type.GetProperties().Where(x => x.PropertyType.IsPrimitive() && x.PropertyType != typeof(Guid)).ToArray();
            //and the virtual properties
            var virtualProperties = type.GetProperties().Where(x => !x.PropertyType.IsPrimitive()).ToArray();
            //create a product work sheet
            var sheet = workbook.GetSheet(type.Name);
            if (sheet == null)
            {
                sheet = workbook.CreateSheet(type.Name);
                //top line should be headings
                var headingRow = sheet.CreateRow(0);
                for (var index = 0; index < dbProperties.Length; index++)
                {
                    var headingCell = headingRow.CreateCell(index);
                    headingCell.SetCellValue(dbProperties[index].Name);
                }

            }

            var enumerable = entities as object[] ?? entities.Cast<object>().ToArray();
            var enumerator = enumerable.GetEnumerator();
            
            while (enumerator.MoveNext())
            {
                var entity = enumerator.Current;
                if (writtenObjects.Contains(entity))
                    continue;
                
                CreateRow(entity, sheet, sheet.LastRowNum + 1, dbProperties);
                writtenObjects.Add(entity);
             
                foreach (var vp in virtualProperties)
                {
                    var propertyType = vp.PropertyType;
                    if (propertyType != typeof(string) && typeof(IEnumerable).IsAssignableFrom(vp.PropertyType))
                    {
                        var vpObject = (IEnumerable)vp.GetValue(entity);
                        var genericType = propertyType.GetGenericArguments().First();
                        FillSheet(workbook, genericType, vpObject);
                    }
                    else
                    {
                        var vpObject = vp.GetValue(entity);
                        if (vpObject != null)
                            FillSheet(workbook, propertyType, new[] { vpObject });
                    }
                }
            }
            return sheet;
        }

        private IRow CreateRow(object entity, ISheet sheet, int index, PropertyInfo[] dbProperties)
        {
            var newRow = sheet.CreateRow(index);
            for (var cellIndex = 0; cellIndex < dbProperties.Length; cellIndex++)
            {
                var rowCell = newRow.CreateCell(cellIndex);
                var property = dbProperties[cellIndex];
                SetValue(rowCell, property.GetValue(entity), property.PropertyType);
            }
            return newRow;
        }

        private IList<T> ReadSheet<T>(Mapper mapper) where T : class
        {
            var rowInfos = mapper.Take<T>(typeof(T).Name);
            return rowInfos.Select(x => x.Value).ToList();
        }

        private object CreateObject(IRow row, Type type, Dictionary<string, int> headingIndices, PropertyInfo[] dbProperties)
        {
            var obj = Activator.CreateInstance(type);
            foreach (var property in dbProperties)
            {
                var cell = row.GetCell(headingIndices[property.Name]);
              
                switch (cell.CellType)
                {
                    case CellType.Boolean:
                        property.SetValue(obj, cell.BooleanCellValue);
                        break;
                    case CellType.Numeric:
                        property.SetValue(obj, (decimal) cell.NumericCellValue);
                        break;
                    default:
                        property.SetValue(obj, cell.StringCellValue);
                        break;
                }
                
            }

            return obj;
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

        private void SetValue(ICell cell, object value, Type type)
        {
            if (value == null)
            {
                return;
            }
            else if (type == typeof(DateTime) || type == typeof(DateTime?))
            {
                cell.SetCellValue(((DateTime)value).ToString(DateFormat));
            }
            else if (type == typeof(bool) || type == typeof(bool?))
            {
                cell.SetCellValue((bool) value);
            }
            else if (type == typeof(decimal) || type == typeof(decimal?))
            {
                cell.SetCellType(CellType.Numeric);
                cell.SetCellValue((double)Convert.ChangeType(value, TypeCode.Double));
            }
            else if (type == typeof(int) || type == typeof(int?))
            {
                cell.SetCellValue((double)Convert.ChangeType(value, TypeCode.Double));
            }
            else
            {
                cell.SetCellValue(value.ToString());
            }
        }

    }
}