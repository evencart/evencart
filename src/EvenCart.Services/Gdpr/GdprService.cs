using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DotEntity;
using DotEntity.Enumerations;
using EvenCart.Data.Entity.Gdpr;
using EvenCart.Services.Security;
using EvenCart.Services.Users;

namespace EvenCart.Services.Gdpr
{
    public class GdprService : IGdprService
    {
        private readonly IConsentLogService _consentLogService;
        private readonly IConsentService _consentService;
        private readonly ICryptographyService _cryptographyService;
        private readonly IUserService _userService;
        public GdprService(IConsentLogService consentLogService, IConsentService consentService, ICryptographyService cryptographyService, IUserService userService)
        {
            _consentLogService = consentLogService;
            _consentService = consentService;
            _cryptographyService = cryptographyService;
            _userService = userService;
        }

        public void SetUserConsents(int userId, Dictionary<int, ConsentStatus> consentPairs)
        {
            var allConsents = _consentService.Get(x => x.Published).ToList();

            var savedConsents = GetUserConsents(userId);
            var user = _userService.Get(userId);
            IList<ConsentLog> logs = new List<ConsentLog>();
            foreach (var (consentId, consentStatus) in consentPairs)
            {
                var consent = allConsents.FirstOrDefault(x => x.Id == consentId);
                if (consent == null || (consent.IsRequired && consentStatus != ConsentStatus.Accepted))
                    continue;
                var savedConsent = savedConsents.FirstOrDefault(x => x.ConsentId == consentId);
                var log = false;
                if (savedConsent == null)
                {
                    savedConsent = new UserConsent()
                    {
                        ConsentId = consentId,
                        UserId =  userId,
                        ConsentStatus = consentStatus
                    };
                    EntitySet<UserConsent>.Insert(savedConsent);
                    log = true;
                }
                else if (savedConsent.ConsentStatus != consentStatus)
                {
                    savedConsent.ConsentStatus = consentStatus;
                    EntitySet<UserConsent>.Update(savedConsent);
                    log = true;
                }

                if (consent.EnableLogging && log)
                {
                    //add to log
                    logs.Add(new ConsentLog()
                    {
                        ActivityType = consentStatus == ConsentStatus.Accepted ? ActivityType.ConsentAccepted : ActivityType.ConsentDenied,
                        ConsentId = consentId,
                        UserId = userId,
                        CreatedOn = DateTime.UtcNow,
                        EncryptedUserInfo = _cryptographyService.Encrypt(user.Email)
                    });
                }
                
            }

            //commit the logs to db
            _consentLogService.Insert(logs.ToArray());
        }

        public IList<UserConsent> GetUserConsents(int userId)
        {
            return EntitySet<UserConsent>.Where(x => x.UserId == userId)
                .Select().ToList();
        }

        public IList<Consent> GetPendingConsents(int userId)
        {
            Expression<Func<UserConsent, bool>> userWhere = consent => consent.UserId == userId || consent.UserId == null;
            //though integer can't be null
            //and so here the comparison might not make sense, but in sql server, because it's a join,
            //the user id of the columns will be null and so it's necessary to include this check above

            return EntitySet<Consent>.Just().Join<UserConsent>("Id", "ConsentId", joinType: JoinType.LeftOuter)
                .Relate<UserConsent>((consent, userConsent) =>
                {
                    consent.Tag = true; /*setting tag to true for all the consents with right values*/
                })
                .Where(userWhere)
                .SelectNested()
                .Where(x => x.Tag == null).ToList();
        }

        public bool IsConsentAccepted(int userId, int consentId)
        {
            return EntitySet<UserConsent>.Where(x => x.UserId == userId && x.ConsentId == consentId && x.ConsentStatus == ConsentStatus.Accepted).Count() > 0;
        }

        public bool AreConsentsActedUpon(int userId, params int[] consentIds)
        {
            var actedUponConsents = EntitySet<UserConsent>.Where(x => x.UserId == userId).Select().ToList();
            foreach (var consentId in consentIds)
            {
                if (actedUponConsents.All(x => x.ConsentId != consentId))
                {
                    return false;
                }
            }
            return true;
        }
    }
}