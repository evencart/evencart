using System;
using System.Linq;
using EvenCart.Core.Services.Events;
using EvenCart.Data.Entity.Purchases;
using EvenCart.Data.Entity.Settings;
using EvenCart.Data.Entity.Users;
using EvenCart.Data.Extensions;
using EvenCart.Infrastructure;
using EvenCart.Services.Users;

namespace EvenCart.Events
{
    public class AffiliateEventsCapture : IEventCapture
    {
        private readonly AffiliateSettings _affiliateSettings;
        private readonly IStoreCreditService _storeCreditService;
        private readonly OrderSettings _orderSettings;
        public AffiliateEventsCapture(AffiliateSettings affiliateSettings, IStoreCreditService storeCreditService, OrderSettings orderSettings)
        {
            _affiliateSettings = affiliateSettings;
            _storeCreditService = storeCreditService;
            _orderSettings = orderSettings;
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
                        _storeCreditService.Insert(new StoreCredit()
                        {
                            CreatedOn = DateTime.UtcNow,
                            AvailableOn = DateTime.UtcNow,
                            Credit = _affiliateSettings.SignupCreditToAffiliate,
                            Description = $"{user.Name} - {user.Email} account was activated",
                            UserId = user.ReferrerId
                        });
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
                    var affiliateUserId = ApplicationEngine.CurrentAffiliate?.Id ?? 0;
                    
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