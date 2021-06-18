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

using Genesis.Services.Events;
using EvenCart.Data.Entity.Payments;
using EvenCart.Services.Orders;

namespace EvenCart.Services.Captures
{
    public class PaymentTransactionCapture : IGenesisEntityInserted<PaymentTransaction>
    {
        private readonly IPurchaseAccountant _purchaseAccountant;
        private readonly IOrderService _orderService;
        public PaymentTransactionCapture(IPurchaseAccountant purchaseAccountant, IOrderService orderService)
        {
            _purchaseAccountant = purchaseAccountant;
            _orderService = orderService;
        }

        public void OnInserted(PaymentTransaction entity)
        {
            //get the order
            var order = entity.Order ?? _orderService.GetByGuid(entity.OrderGuid);
            if (order == null)
                return; //do nothing, we don't have the order

            order.PaymentStatus = entity.PaymentStatus;
            _purchaseAccountant.EvaluateOrderStatus(order);
        }
    }
}