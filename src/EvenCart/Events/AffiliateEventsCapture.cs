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
using System.Linq;
using EvenCart.Data.Entity.Purchases;
using EvenCart.Data.Entity.Settings;
using Genesis.Infrastructure;
using Genesis.Modules.Settings;
using Genesis.Modules.Stores;
using Genesis.Modules.Users;
using Genesis.Services.Events;

namespace EvenCart.Events
{
    public class AffiliateEventsCapture : IEventCapture
    {
        private readonly AffiliateSettings _affiliateSettings;
        private readonly IStoreCreditService _storeCreditService;
        private readonly OrderSettings _orderSettings;
        private readonly IGenesisEngine _appEngine;
        public AffiliateEventsCapture(AffiliateSettings affiliateSettings, IStoreCreditService storeCreditService, OrderSettings orderSettings, IGenesisEngine appEngine)
        {
            _affiliateSettings = affiliateSettings;
            _storeCreditService = storeCreditService;
            _orderSettings = orderSettings;
            _appEngine = appEngine;
        }

        public void Capture(string eventName, object[] eventData = null)
        {
            if (!_affiliateSettings.EnableAffiliates || eventData == null)
                return;
            switch (eventName)
            {
                case nameof(NamedEvent.UserActivated):
                    var user = (User) eventData[0];
                    if (_affiliateSettings.SignupCreditToAffiliate > 0)
                    {
                        if (user.ReferrerId > 0)
                        {
                            _storeCreditService.Insert(new StoreCredit()
                            {
                                CreatedOn = DateTime.UtcNow,
                                AvailableOn = DateTime.UtcNow,
                                Credit = _affiliateSettings.SignupCreditToAffiliate,
                                Description = $"{user.Name} account was activated",
                                UserId = user.ReferrerId
                            });
                        }
                       
                    }
                    if (_affiliateSettings.SignupCreditToNewUser > 0)
                    {
                        _storeCreditService.Insert(new StoreCredit()
                        {
                            CreatedOn = DateTime.UtcNow,
                            AvailableOn = DateTime.UtcNow,
                            Credit = _affiliateSettings.SignupCreditToNewUser,
                            Description = $"Account activation credit",
                            UserId = user.Id
                        });
                    }
                    break;
                case nameof(NamedEvent.OrderPaid):
                    var order = (Order) eventData[1];
                    var amount = _affiliateSettings.ExcludeTaxFromCalculation
                        ? order.OrderTotal - order.Tax
                        : order.OrderTotal;
                    var affiliateAmount = _affiliateSettings.GetAffiliateAmount(amount);
                    var affiliateUserId = _appEngine.CurrentAffiliate?.Id ?? 0;
                    
                    if (affiliateUserId > 0)
                    {
                        //the credit should be available only after return period is over
                        var availableOn = DateTime.UtcNow;
                        if (_orderSettings.AllowReturns)
                        {
                            var returnDays = order.OrderItems.Where(x => x.Product.AllowReturns)
                                .Max(x => x.Product.DaysForReturn);
                            if (returnDays == 0)
                                returnDays = _orderSettings.DefaultReturnsDays;
                            availableOn = availableOn.AddDays(returnDays);
                        }
                        _storeCreditService.Insert(new StoreCredit()
                        {
                            CreatedOn = DateTime.UtcNow,
                            AvailableOn = availableOn,
                            Credit = affiliateAmount,
                            Description = $"Order # {order.Guid} paid by {order.User.Name}",
                            UserId = affiliateUserId
                        });
                    }
                    break;
            }
        }

        public string[] EventNames { get; } =
        {
            NamedEvent.UserActivated.ToString(),
            NamedEvent.OrderPaid.ToString()
        };
    }
}