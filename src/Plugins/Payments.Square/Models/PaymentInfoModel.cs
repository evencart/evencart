using EvenCart.Infrastructure.Mvc.Models;

namespace Payments.Square.Models
{
    public class PaymentInfoModel : FoundationModel
    {
        public string ApplicationId { get; set; }

        public string PostalCode { get; set; }

        public string ScriptUrl { get; set; }
    }
}