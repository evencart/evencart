using System;
using System.Collections.Generic;
using System.Linq;
using EvenCart.Areas.Administration.Models.Reports;
using EvenCart.Data.Entity.Purchases;
using EvenCart.Data.Entity.Shop;
using EvenCart.Data.Enum;
using EvenCart.Infrastructure.Extensions;

namespace EvenCart.Areas.Administration.Factories.Reports
{
    public class ReportModelFactory : IReportModelFactory
    {
        public StockReportModel Create(Product entity)
        {
            var model = new StockReportModel()
            {
                ProductId = entity.Id,
                Published = entity.Published,
                StockQuantity = entity.StockQuantity,
                ProductName = entity.Name,
                HasVariants = entity.HasVariants
            };

            if (entity.HasVariants)
            {
                model.Variants = entity.ProductVariants.Select(variant =>
                {
                    return new StockReportModel.VariantStockReportModel()
                    {
                        StockQuantity = variant.StockQuantity ?? 0,
                        AttributeText = string.Join('\n',
                            variant.ProductVariantAttributes.Select(x =>
                                $"{x.ProductAttribute.Label}: {x.ProductAttributeValue.Label}"))
                    };
                }).ToList();
            }
            return model;
        }

        public IList<OrderReportModel> CreateOrderReportModels(out int totalResults, IList<Order> orders, GroupUnit groupBy, int page = 1, int count = 15)
        {
            orders = orders.OrderBy(x => x.CreatedOn).ToList();
            IEnumerable<IGrouping<string, Order>> groupedOrders;
            switch (groupBy)
            {
                case GroupUnit.Days:
                    groupedOrders = orders.GroupBy(x => x.CreatedOn.ToFormattedString());
                    break;
                case GroupUnit.Months:
                    groupedOrders = orders.GroupBy(x => $"{x.CreatedOn.GetMonthName()} {x.CreatedOn.Year}");
                    break;
                case GroupUnit.Years:
                    groupedOrders = orders.GroupBy(x => $"{x.CreatedOn.Year}");
                    break;
                case GroupUnit.Weeks:
                default:
                    var minDate = orders.Min(x => x.CreatedOn);
                    var maxDate = orders.Max(x => x.CreatedOn);
                    groupedOrders = orders.GroupBy(x =>
                    {
                        x.CreatedOn.GetWeekRangeDates(out var startDate, out var endDate);
                        if (startDate < minDate)
                            startDate = minDate;
                        if (endDate > maxDate)
                            endDate = maxDate;
                        return $"{startDate.ToFormattedString()} - {endDate.ToFormattedString()}";
                    });
                    break;
            }

            var orderReports = new List<OrderReportModel>();
            foreach (var go in groupedOrders)
            {
                var groupOrders = go.ToList();
                var groupName = go.Key;
                orderReports.Add(new OrderReportModel()
                {
                    GroupName = groupName,
                    TotalOrders = groupOrders.Count,
                    TotalProducts = groupOrders.SelectMany(x => x.OrderItems).Count(),
                    TotalAmount = groupOrders.Sum(x => x.OrderTotal)
                });
            }

            totalResults = orderReports.Count;
            return orderReports.Skip(count * (page - 1)).Take(count).ToList();
        }

        public IList<UserOrderReportModel> CreateUserOrderReportModels(out int totalResults, IList<Order> orders, int page, int count)
        {
            var groupedOrders = orders.GroupBy(x => x.User);
            var orderReports = new List<UserOrderReportModel>();
            foreach (var go in groupedOrders)
            {
                var groupOrders = go.ToList();
                var user = go.Key;
                orderReports.Add(new UserOrderReportModel()
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    TotalOrders = groupOrders.Count,
                    TotalProducts = groupOrders.SelectMany(x => x.OrderItems).Count(),
                    TotalAmount = groupOrders.Sum(x => x.OrderTotal),
                });
            }

            totalResults = orderReports.Count;
            return orderReports.OrderByDescending(x => x.TotalAmount).Skip(count * (page - 1)).Take(count).ToList();
        }

        public IList<ProductOrderReportModel> CreateProductOrderReportModels(out int totalResults, IList<Order> orders, int page = 1, int count = 15)
        {
            var orderItems = orders.SelectMany(x => x.OrderItems);
            var groupedItems = orderItems.GroupBy(x => x.Product);

            var productReports = new List<ProductOrderReportModel>();
            foreach (var go in groupedItems)
            {
                var groupItems = go.ToList();
                var product = go.Key;
                productReports.Add(new ProductOrderReportModel()
                {
                    Id = product.Id,
                    Name = product.Name,
                    TotalOrders = groupItems.Count,
                    TotalCustomers = groupItems.Select(x => x.Order).Select(x => x.UserId).Distinct().Count()
                });
            }

            totalResults = productReports.Count;
            return productReports.OrderByDescending(x => x.TotalOrders).Skip(count * (page - 1)).Take(count).ToList();
        }
    }
}