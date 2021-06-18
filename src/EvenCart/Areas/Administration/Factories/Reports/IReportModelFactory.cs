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

using System.Collections.Generic;
using EvenCart.Areas.Administration.Models.Reports;
using EvenCart.Data.Entity.Purchases;
using EvenCart.Data.Entity.Shop;
using Genesis.Infrastructure.Mvc.ModelFactories;
using Genesis.Modules.Meta;
using Genesis.Modules.Users;

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