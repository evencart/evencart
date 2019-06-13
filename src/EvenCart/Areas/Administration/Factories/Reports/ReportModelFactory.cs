using System;
using System.Collections.Generic;
using System.Linq;
using EvenCart.Areas.Administration.Models.Reports;
using EvenCart.Data.Entity.Purchases;
using EvenCart.Data.Entity.Shop;
using EvenCart.Data.Entity.Users;
using EvenCart.Data.Enum;
using EvenCart.Infrastructure.Extensions;
using EvenCart.Services.Helpers;

namespace EvenCart.Areas.Administration.Factories.Reports
{
    public class ReportModelFactory : IReportModelFactory
    {
        public StockReportModel Create(Product entity)
        {
            var inventory = entity.Inventories?.FirstOrDefault() ?? new WarehouseInventory() { ReservedQuantity =  0, TotalQuantity = 0};
            var model = new StockReportModel()
            {
                ProductId = entity.Id,
                Published = entity.Published,
                StockQuantity = inventory.AvailableQuantity,
                ReservedQuantity = inventory.ReservedQuantity,
                ProductName = entity.Name,
                HasVariants = entity.HasVariants
            };

            if (entity.HasVariants)
            {
                model.Variants = entity.ProductVariants.Select(variant =>
                {
                    var variantInventory = variant.Inventories?.FirstOrDefault();
                    return new StockReportModel.VariantStockReportModel()
                    {
                        StockQuantity = variantInventory?.AvailableQuantity ?? 0,
                        ReservedQuantity = variantInventory?.ReservedQuantity ?? 0,
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
            totalResults = 0;
            if (!orders.Any())
                return null;
            orders = orders.OrderBy(x => x.CreatedOn).ToList();
            IEnumerable<IGrouping<string, Order>> groupedOrders;
            List<string> allValidGroupNames = null;
            var minDate = orders.Min(x => x.CreatedOn);
            var maxDate = orders.Max(x => x.CreatedOn);
            var allDates = DateTimeHelper.DatesBetween(minDate, maxDate);
            switch (groupBy)
            {
                case GroupUnit.Days:
                    groupedOrders = orders.GroupBy(x => x.CreatedOn.ToFormattedString());
                    allValidGroupNames = allDates.Select(x => x.ToFormattedString()).ToList();
                    break;
                case GroupUnit.Months:
                    groupedOrders = orders.GroupBy(x => $"{x.CreatedOn.GetMonthName()} {x.CreatedOn.Year}");
                    allValidGroupNames = allDates.Select(x => $"{x.GetMonthName()} {x.Year}").Distinct().ToList();
                    break;
                case GroupUnit.Years:
                    groupedOrders = orders.GroupBy(x => $"{x.CreatedOn.Year}");
                    allValidGroupNames = allDates.Select(x => $"{x.Year}").Distinct().ToList();
                    break;
                case GroupUnit.Weeks:
                default:
                    minDate.GetWeekRangeDates(out var minWeekDate, out var maxWeekDate);
                    var weekStartDate = minWeekDate;
                    allValidGroupNames = new List<string>();
                    while (weekStartDate < maxDate)
                    {
                        allValidGroupNames.Add($"{weekStartDate.ToFormattedString()} - {weekStartDate.AddDays(6).ToFormattedString()}");
                        //next week
                        weekStartDate = weekStartDate.AddDays(7);
                    }
                    groupedOrders = orders.GroupBy(x =>
                    {
                        x.CreatedOn.GetWeekRangeDates(out var startDate, out var endDate);
                        return $"{startDate.ToFormattedString()} - {endDate.ToFormattedString()}";
                    });
                    break;
            }

            var groupedOrdersAsList = groupedOrders.ToList();
            var orderReports = new List<OrderReportModel>();
            foreach (var groupName in allValidGroupNames)
            {
                var reportModel = new OrderReportModel()
                {
                    GroupName = groupName,
                    TotalOrders = 0,
                    TotalProducts = 0,
                    TotalAmount = 0
                };
                var group = groupedOrdersAsList.FirstOrDefault(x => x.Key == groupName);
                if (group != null)
                {
                    var groupOrders = group.ToList();
                    reportModel.TotalOrders = groupOrders.Count;
                    reportModel.TotalProducts = groupOrders.SelectMany(x => x.OrderItems).Count();
                    reportModel.TotalAmount = groupOrders.Sum(x => x.OrderTotal);
                }
                orderReports.Add(reportModel);
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

        public IList<UserRegistrationReportModel> CreateUserRegistrationReportModels(out int totalResults,  IList<User> users, GroupUnit groupBy, int page = 1, int count = 15)
        {
            var minDate = users.Min(x => x.CreatedOn);
            var maxDate = users.Max(x => x.CreatedOn);
            var allDates = DateTimeHelper.DatesBetween(minDate, maxDate);
            IEnumerable<IGrouping<string, User>> groupedUsers;
            List<string> allValidGroupNames = null;
            switch (groupBy)
            {
                case GroupUnit.Days:
                    groupedUsers = users.GroupBy(x => x.CreatedOn.ToFormattedString());
                    allValidGroupNames = allDates.Select(x => x.ToFormattedString()).ToList();
                    break;
                case GroupUnit.Months:
                    groupedUsers = users.GroupBy(x => $"{x.CreatedOn.GetMonthName()} {x.CreatedOn.Year}");
                    allValidGroupNames = allDates.Select(x => $"{x.GetMonthName()} {x.Year}").Distinct().ToList();
                    break;
                case GroupUnit.Years:
                    groupedUsers = users.GroupBy(x => $"{x.CreatedOn.Year}");
                    allValidGroupNames = allDates.Select(x => $"{x.Year}").Distinct().ToList();
                    break;
                case GroupUnit.Weeks:
                default:
                    minDate.GetWeekRangeDates(out var minWeekDate, out var maxWeekDate);
                    var weekStartDate = minWeekDate;
                    allValidGroupNames = new List<string>();
                    while (weekStartDate < maxDate)
                    {
                        allValidGroupNames.Add($"{weekStartDate.ToFormattedString()} - {weekStartDate.AddDays(6).ToFormattedString()}");
                        //next week
                        weekStartDate = weekStartDate.AddDays(7);
                    }
                    groupedUsers = users.GroupBy(x =>
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

            var registrationReports = new List<UserRegistrationReportModel>();
            var groupedUsersAsList = groupedUsers.ToList();
            foreach (var groupName in allValidGroupNames)
            {
                var reportModel = new UserRegistrationReportModel()
                {
                    GroupName = groupName,
                    TotalUsers = 0
                };
                var group = groupedUsersAsList.FirstOrDefault(x => x.Key == groupName);
                if (group != null)
                {
                    var groupUsers = group.ToList();
                    reportModel.TotalUsers = groupUsers.Count;
                }
                registrationReports.Add(reportModel);
            }

            totalResults = registrationReports.Count;
            return registrationReports.Skip(count * (page - 1)).Take(count).ToList();
        }
    }
}