using EvenCart.Data.Entity.Purchases;
using EvenCart.Infrastructure.Mvc.Models;

namespace EvenCart.Models.Orders
{
    public class ReturnRequestInfoModel : FoundationEntityModel
    {
        public ReturnRequestStatus ReturnRequestStatus { get; set; }

        public ReturnOption ReturnOption { get; set; }

        public string ProductName { get; set; }

        public int ProductId { get; set; }

        public string SeName { get; set; }

        public string AttributeText { get; set; }

        public string ImageUrl { get; set; }

        public int Quantity { get; set; }

        public string ReturnReason { get; set; }

        public string ReturnAction { get; set; }

        public string OrderNumber { get; set; }

        public string OrderGuid { get; set; }

        public string CustomerComments { get; set; }

        public string StaffComments { get; set; }

        public string Remarks { get; set; }

        public string ReturnOrderGuid { get; set; }

        public string ReturnOrderNumber { get; set; }
    }
}