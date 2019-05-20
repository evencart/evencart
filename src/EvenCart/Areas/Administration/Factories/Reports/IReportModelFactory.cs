using System.Collections.Generic;
using EvenCart.Areas.Administration.Models.Reports;
using EvenCart.Data.Entity.Purchases;
using EvenCart.Data.Entity.Shop;
using EvenCart.Data.Entity.Users;
using EvenCart.Data.Enum;
using EvenCart.Infrastructure.Mvc.ModelFactories;

namespace EvenCart.Areas.Administration.Factories.Reports
{
    public interface IReportModelFactory : IModelFactory<Product, StockReportModel>
    {
        IList<OrderReportModel> CreateOrderReportModels(out int totalResults, IList<Order> orders, GroupUnit groupBy, int page = 1, int count = 15);

        IList<UserOrderReportModel> CreateUserOrderReportModels(out int totalResults, IList<Order> orders, int page = 1, int count = 15);

        IList<ProductOrderReportModel> CreateProductOrderReportModels(out int totalResults, IList<Order> orders, int page = 1, int count = 15);

        IList<UserRegistrationReportModel> CreateUserRegistrationReportModels(out int totalResults, IList<User> users, GroupUnit groupBy, int page = 1, int count = 15);
    }
}