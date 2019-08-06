using System.Collections.Generic;
using EvenCart.Data.Entity.Purchases;

namespace EvenCart.Data.Entity.Payments
{
    /// <summary>
    /// A single request class to implement all types of transaction requests
    /// </summary>
    public class TransactionRequest 
    {
        public Order Order { get; set; }

        public string TransactionGuid { get; set; }

        public Dictionary<string, object> Parameters { get; set; }

        public bool IsPartialRefund { get; set; }

        public TransactionRequestType RequestType { get; set; }

        public decimal? Amount { get; set; }
    }
}