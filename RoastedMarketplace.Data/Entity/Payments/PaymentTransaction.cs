using System;
using System.Collections.Generic;
using RoastedMarketplace.Core.Data;
using Newtonsoft.Json;

namespace RoastedMarketplace.Data.Entity.Payments
{
    public class PaymentTransaction : FoundationEntity, ISoftDeletable
    {
        public string TransactionGuid { get; set; }

        public PaymentStatus PaymentStatus { get; set; }

        public DateTime CreatedOn { get; set; }

        public string OrderGuid { get; set; }

        public decimal TransactionAmount { get; set; }

        public string UserIpAddress { get; set; }

        public string PaymentMethodName { get; set; }

        #region Virtual Properties
        private Dictionary<string, object> _transactionCodes;

        public virtual Dictionary<string, object> TransactionCodes
        {
            get
            {
                _transactionCodes = _transactionCodes ?? new Dictionary<string, object>();
                return _transactionCodes;
            }
            set => _transactionCodes = value;
        }
        #endregion

        //use serialized string to store data
        public string TransactionCodesSerialized => JsonConvert.SerializeObject(TransactionCodes);

        public string TransactionCurrencyCode { get; set; }

        public bool Deleted { get; set; }
    }

}