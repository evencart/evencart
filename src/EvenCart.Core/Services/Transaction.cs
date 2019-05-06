using System;
using DotEntity;

namespace EvenCart.Core.Services
{
    public class Transaction
    {
        internal IDotEntityTransaction Value { get; set; }

        public static void Initiate(Action<Transaction> operationAction)
        {
            using (var t = EntitySet.BeginTransaction())
            {
                var transaction = new Transaction() { Value = t };
                operationAction(transaction);
                t.Commit();
            }
        }
    }
}