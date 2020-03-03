using System.Collections.Generic;
using System.Linq;
using EvenCart.Data.Entity.Payments;
using EvenCart.Data.Entity.Purchases;
using EvenCart.Infrastructure.Mvc;
using EvenCart.Infrastructure.Routing;
using EvenCart.Services.Extensions;
using EvenCart.Services.Products;
using EvenCart.Services.Purchases;
using Microsoft.AspNetCore.Mvc;

namespace EvenCart.Controllers
{
    public class DownloadsController : FoundationController
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
            
            return File(download.FileBytes, download.FileType, $"{download.Title}{download.FileExtension}");
        }
    }
}