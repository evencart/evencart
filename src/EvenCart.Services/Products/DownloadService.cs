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
using System.Linq;
using System.Linq.Expressions;
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