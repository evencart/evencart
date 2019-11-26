using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using EvenCart.Core.Services;
using EvenCart.Data.Entity.Payments;
using EvenCart.Data.Entity.Purchases;
using EvenCart.Data.Entity.Shop;

namespace EvenCart.Services.Products
{
    public interface IDownloadService : IFoundationEntityService<Download>
    {
        Download GetWithoutBytes(int id);

        IEnumerable<Download> GetWithoutBytes(Expression<Func<Download, bool>> where, int page = 1, int count = int.MaxValue);

        void InitializeDownloads(Order order);

        void InitializeDownloads(OrderItem orderItem, PaymentStatus paymentStatus);
    }
}