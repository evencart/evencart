using System.Collections.Generic;
using EvenCart.Core.Exception;
using EvenCart.Data.Entity.Payments;

namespace EvenCart.Services.Extensions
{
    public static class PaymentProcessorExtensions
    {
        public static T GetParameterAs<T>(this TransactionRequest request, string parameterName)
        {
            if (request == null)
                throw new EvenCartException("Can't read a null request");

            if (!request.Parameters.ContainsKey(parameterName))
                return default(T);

            return (T)request.Parameters[parameterName];
        }

        public static T GetParameterAs<T>(this TransactionResult result, string parameterName)
        {
            if (result == null)
                throw new EvenCartException("Can't read a null result");

            if (!result.ResponseParameters.ContainsKey(parameterName))
                return default(T);

            return (T)result.ResponseParameters[parameterName];
        }

        /// <summary>
        /// Sets a parameter in transaction results
        /// </summary>
        /// <param name="result"></param>
        /// <param name="parameterName"></param>
        /// <param name="parameterValue"></param>
        public static void SetParameter(this TransactionResult result, string parameterName, object parameterValue)
        {
            if (result.ResponseParameters.ContainsKey(parameterName))
                result.ResponseParameters[parameterName] = parameterValue;
            else
            {
                result.ResponseParameters.Add(parameterName, parameterValue);
            }
        }

        /// <summary>
        /// Sets a response code in a payment transaction
        /// </summary>
        /// <param name="paymentTransaction"></param>
        /// <param name="parameterName"></param>
        /// <param name="parameterValue"></param>
        public static void SetTransactionCode(this PaymentTransaction paymentTransaction, string parameterName,
            object parameterValue)
        {
            if (paymentTransaction == null)
                throw new EvenCartException("Can't read null payment transaction");

            if (paymentTransaction.TransactionCodes.ContainsKey(parameterName))
                paymentTransaction.TransactionCodes[parameterName] = parameterValue;
            else
                paymentTransaction.TransactionCodes.Add(parameterName, parameterValue);
        }

        /// <summary>
        /// Sets response transaction codes in a payment transaction
        /// </summary>
        /// <param name="paymentTransaction"></param>
        /// <param name="transactionCodes"></param>
        public static void SetTransactionCodes(this PaymentTransaction paymentTransaction,
            Dictionary<string, object> transactionCodes)
        {
            if (transactionCodes != null)
                foreach (var code in transactionCodes)
                {
                    paymentTransaction.SetTransactionCode(code.Key, code.Value);
                }
        }
        /// <summary>
        /// Gets the value of named response code from payment transaction
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="paymentTransaction"></param>
        /// <param name="parameterName"></param>
        /// <returns></returns>
        public static T GetTransactionCodeAs<T>(this PaymentTransaction paymentTransaction, string parameterName)
        {
            if (paymentTransaction == null)
                throw new EvenCartException("Can't read a null result");

            if (!paymentTransaction.TransactionCodes.ContainsKey(parameterName))
                return default(T);

            return (T)paymentTransaction.TransactionCodes[parameterName];
        }
    }
}