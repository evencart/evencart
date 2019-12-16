namespace Payments.Square.Models
{
    public class PaymentReturnModel
    {
        public string PaymentId { get; set; }

        public string Token { get; set; }

        public string PayerId { get; set; }
    }
}