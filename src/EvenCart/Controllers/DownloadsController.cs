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
using System.Linq;
using EvenCart.Data.Entity.Payments;
using EvenCart.Data.Entity.Purchases;
using EvenCart.Services.Orders;
using EvenCart.Services.Products;
using Genesis.Infrastructure.Mvc;
using Genesis.Modules.Users;
using Genesis.Routing;
using Microsoft.AspNetCore.Mvc;

namespace EvenCart.Controllers
{
    public class DownloadsController : GenesisController
    {
        private readonly IDownloadService _downloadService;
        private readonly IOrderService _orderService;
        private readonly IOrderItemDownloadService _orderItemDownloadService;
        public DownloadsController(IDownloadService downloadService, IOrderService orderService, IOrderItemDownloadService orderItemDownloadService)
        {
            _downloadService = downloadService;
            _orderService = orderService;
            _orderItemDownloadService = orderItemDownloadService;
        }

        [HttpGet("downloads/{guid}", Name = RouteNames.DownloadFile)]
        public IActionResult DownloadFile(string guid)
        {
            var download = _downloadService.FirstOrDefault(x => x.Guid == guid);
            if (download == null || !download.Published)
                return NotFound();
            if (download.RequireLogin && CurrentUser.IsVisitor())
                return NotFound();

            if (download.RequirePurchase)
            {
                //find the orders of the user
                var orders = _orderService.GetOrders(out _, userId: CurrentUser.Id, storeId: CurrentStore.Id,
                    productIds: new List<int>() {download.ProductId},
                    paymentStatus: new List<PaymentStatus>() {PaymentStatus.Complete}).ToList();
                if (!orders.Any())
                    return NotFound();
            }

            if (!CurrentUser.IsVisitor())
            {
                //get the download object
                var orderItemDownload = _orderItemDownloadService.FirstOrDefault(x => x.DownloadId == download.Id && x.UserId == CurrentUser.Id);
                if (orderItemDownload != null)
                {
                    if (!orderItemDownload.Active)
                        return NotFound();
                }
                else
                {
                    orderItemDownload = new ItemDownload()
                    {
                        Active = true,
                        DownloadCount = 0,
                        DownloadId = download.Id,
                        UserId = CurrentUser.Id
                    };
                }
                //update the stat
                orderItemDownload.DownloadCount++;
                _orderItemDownloadService.InsertOrUpdate(orderItemDownload);
            }

            download.DownloadCount++;
            _downloadService.Update(download);
            
            return File(download.FileBytes, download.FileType, $"{download.Title}{download.FileExtension}");
        }
    }
}