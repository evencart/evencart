using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DotEntity;
using EvenCart.Core.Services;
using EvenCart.Data.Entity.Payments;
using EvenCart.Data.Entity.Purchases;
using EvenCart.Data.Entity.Shop;

namespace EvenCart.Services.Products
{
    public class DownloadService : FoundationEntityService<Download>, IDownloadService
    {
        private readonly IOrderItemDownloadService _orderItemDownloadService;
        public DownloadService(IOrderItemDownloadService orderItemDownloadService)
        {
            _orderItemDownloadService = orderItemDownloadService;
        }

        public Download GetWithoutBytes(int id)
        {
            return Repository.SkipColumns<Download>(nameof(Download.FileBytes))
                .Where(x => x.Id == id)
                .SelectSingle();
        }

        public IEnumerable<Download> GetWithoutBytes(Expression<Func<Download, bool>> @where, int page = 1, int count = Int32.MaxValue)
        {
            return Repository.SkipColumns<Download>(nameof(Download.FileBytes))
                .Where(where)
                .OrderBy(x => x.DisplayOrder)
                .Select(page, count);
        }

        public void InitializeDownloads(Order order)
        {
            Transaction.Initiate(transaction =>
            {
                var productIds = order.OrderItems.Where(x => x.IsDownloadable).Select(x => x.ProductId).ToList();
                if (!productIds.Any())
                    return;
                var allDownloads = GetWithoutBytes(x => productIds.Contains(x.ProductId)).ToList();
                foreach (var orderItem in order.OrderItems.Where(x => x.IsDownloadable))
                {
                    var downloads = allDownloads.Where(x => x.ProductId == orderItem.ProductId).ToList();
                    foreach (var download in downloads)
                    {
                        if (download.ProductVariantId > 0 && orderItem.ProductVariantId != download.ProductVariantId)
                            continue; //skip this download
                        var orderItemDownload = new ItemDownload()
                        {
                            DownloadId = download.Id,
                            Active = download.DownloadActivationType != DownloadActivationType.Manual &&
                                     order.PaymentStatus == PaymentStatus.Complete,
                            UserId = order.UserId
                        };
                        _orderItemDownloadService.Insert(orderItemDownload, transaction);
                    }
                }
            });
        }

        public void InitializeDownloads(OrderItem orderItem, PaymentStatus paymentStatus)
        {
            var downloads = GetWithoutBytes(x => x.ProductId == orderItem.ProductId).ToList();
            Transaction.Initiate(transaction =>
            {
                foreach (var download in downloads)
                {
                    if (download.ProductVariantId > 0 && orderItem.ProductVariantId != download.ProductVariantId)
                        continue; //skip this download
                    var orderItemDownload = new ItemDownload()
                    {
                        DownloadId = download.Id,
                        Active = download.DownloadActivationType != DownloadActivationType.Manual && paymentStatus == PaymentStatus.Complete
                    };
                    _orderItemDownloadService.Insert(orderItemDownload, transaction);
                }
            });
        }
    }
}