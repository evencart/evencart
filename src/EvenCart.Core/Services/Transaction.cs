using System;
using DotEntity;

namespace EvenCart.Core.Services
{
    public class Transaction
    {
        internal IDotEntityTransaction Value { get; set; }

        private bool RevertTransaction { get; set; }

        public void Fail()
        {
            RevertTransaction = true;
        }
        public static bool Initiate(Action<Transaction> operationAction)
        {
            using (var t = EntitySet.BeginTransaction())
            {
                var transaction = new Transaction() { Value = t };
                operationAction(transaction);
                if (transaction.RevertTransaction)
                {
                    //don't do anything because transaction was stopped
                    return false;
                }
                t.Commit();
                return t.Success;
            }
        }
    }
}