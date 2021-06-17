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
using System.Collections.Generic;
using System.Linq.Expressions;
using Genesis.Services;
using EvenCart.Data.Entity.Payments;
using EvenCart.Data.Entity.Purchases;
using EvenCart.Data.Entity.Shop;

namespace EvenCart.Services.Products
{
    public interface IDownloadService : IGenesisEntityService<Download>
    {
        Download GetWithoutBytes(int id);

        IEnumerable<Download> GetWithoutBytes(Expression<Func<Download, bool>> where, int page = 1, int count = int.MaxValue);

        void InitializeDownloads(Order order);

        void InitializeDownloads(OrderItem orderItem, PaymentStatus paymentStatus);
    }
}