﻿#region License
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
using Genesis.Services;
using EvenCart.Data.Entity.Payments;
using EvenCart.Data.Entity.Purchases;
using EvenCart.Services.Extensions;
using EvenCart.Services.Orders;
using Genesis;
using Genesis.Extensions;
using Genesis.Modules.Logging;
using Genesis.Modules.Settings;
using Genesis.Modules.Stores;

namespace EvenCart.Services.Payments
{
    [AutoResolvable]
    public class DefaultPaymentAccountant : IPaymentAccountant
    {
        private readonly ILogger _logger;
        private readonly IOrderService _orderService;
        private readonly IPaymentTransactionService _paymentTransactionService;
        private readonly ICartService _cartService;
        private readonly IStoreCreditService _storeCreditService;
        private readonly AffiliateSettings _affiliateSettings;
        public DefaultPaymentAccountant(ILogger logger, IOrderService orderService, IPaymentTransactionService paymentTransactionService, ICartService cartService, IStoreCreditService storeCreditService, AffiliateSettings affiliateSettings)
        {
            _logger = logger;
            _orderService = orderService;
            _paymentTransactionService = paymentTransactionService;
            _cartService = cartService;
            _storeCreditService = storeCreditService;
            _affiliateSettings = affiliateSettings;
        }

        public void ProcessTransactionResult(TransactionResult result, bool clearCart = false)
        {
            var order = result.Order ?? _orderService.GetByGuid(result.OrderGuid);
            if (!result.Success)
            {
                _logger.LogError<Order>(result.Exception, "Error occured while processing payment", order.User, result.ResponseParameters);
                return;
            }
            var paymentTransaction = new PaymentTransaction()
            {
                CreatedOn = DateTime.UtcNow,
                OrderGuid = order.Guid,
                PaymentMethodName = result.IsStoreCreditTransaction ? "Store Credits" : result.IsOfflineTransaction ? "Offline" : order.PaymentMethodName,
                PaymentStatus = result.NewStatus,
                UserIpAddress = order.UserIpAddress,
                TransactionAmount = result.TransactionAmount,
                TransactionGuid = result.TransactionGuid,
                TransactionCurrencyCode = result.TransactionCurrencyCode,
                Order = order
            };
            if (paymentTransaction.TransactionGuid.IsNullEmptyOrWhiteSpace())
                paymentTransaction.TransactionGuid = Guid.NewGuid().ToString();

            paymentTransaction.SetTransactionCodes(result.ResponseParameters);
            //save this
            _paymentTransactionService.Insert(paymentTransaction);


            if (result.IsSubscription && result.NewStatus == PaymentStatus.Complete && !order.IsSubscriptionActive)
            {
                order.IsSubscriptionActive = true;
                _orderService.Update(order);
            }

            //update store credits if required
            if (result.NewStatus == PaymentStatus.Authorized || result.NewStatus == PaymentStatus.Complete)
            {
                //do we need to process credits?
                if (order.UsedStoreCredits)
                {
                    
                    Transaction.Initiate(transaction =>
                    {
                        //unlock the store credits first
                        _storeCreditService.UnlockCredits(order.StoreCredits, order.UserId, transaction);
                        _storeCreditService.Insert(new StoreCredit()
                        {
                            AvailableOn = DateTime.UtcNow,
                            CreatedOn = DateTime.UtcNow,
                            Credit = -order.StoreCredits,
                            Description = "Payment for order #" + order.Guid,
                            UserId = order.UserId
                        }, transaction);

                        paymentTransaction = new PaymentTransaction()
                        {
                            CreatedOn = DateTime.UtcNow,
                            OrderGuid = order.Guid,
                            PaymentMethodName = "Store Credits - " + order.StoreCredits,
                            PaymentStatus = PaymentStatus.Complete,
                            UserIpAddress = order.UserIpAddress,
                            TransactionAmount = order.StoreCreditAmount,
                            TransactionGuid = Guid.NewGuid().ToString(),
                            TransactionCurrencyCode = order.CurrencyCode,
                            Order = order
                        };
                        if (paymentTransaction.TransactionGuid.IsNullEmptyOrWhiteSpace())
                            paymentTransaction.TransactionGuid = Guid.NewGuid().ToString();

                        paymentTransaction.SetTransactionCodes(result.ResponseParameters);
                        //save this
                        _paymentTransactionService.Insert(paymentTransaction, transaction);
                    });

                }
            }

            if (result.NewStatus == PaymentStatus.Refunded || result.NewStatus == PaymentStatus.RefundedPartially)
            {
                if (result.IsStoreCreditTransaction)
                {
                    //and store credits
                    _storeCreditService.Insert(new StoreCredit()
                    {
                        AvailableOn = DateTime.UtcNow,
                        CreatedOn = DateTime.UtcNow,
                        Credit = _affiliateSettings.StoreCreditsExchangeRate > 0 ? result.TransactionAmount / _affiliateSettings.StoreCreditsExchangeRate : 0,
                        Description = "Refund for order #" + order.Guid,
                        UserId = order.UserId
                    });
                }
            }
            //clear cart
            if (clearCart)
                _cartService.ClearCart(order.UserId);
        }
    }
}