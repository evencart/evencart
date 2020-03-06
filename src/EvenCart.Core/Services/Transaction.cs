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