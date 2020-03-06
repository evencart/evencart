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

using System.Linq;
using EvenCart.Data.Entity.Gdpr;
using EvenCart.Models.Gdpr;

namespace EvenCart.Factories.Gdpr
{
    public class GdprModelFactory : IGdprModelFactory
    {
        public ConsentModel Create(Consent entity)
        {
            var consentModel = new ConsentModel()
            {
                Title = entity.Title,
                Description = entity.Description,
                IsRequired = entity.IsRequired,
                OneTimeSelection = entity.OneTimeSelection,
                Id = entity.Id
            };
            return consentModel;
        }

        public ConsentGroupModel Create(ConsentGroup entity)
        {
            var consentGroupModel = new ConsentGroupModel()
            {
                Name = entity.Name,
                Description = entity.Description,
                Id = entity.Id
            };
            if (entity.Consents?.Any() ?? false)
                consentGroupModel.Consents = entity.Consents.Select(Create).ToList();
            return consentGroupModel;
        }

        public ConsentModel Create(Consent entity, ConsentStatus consentStatus)
        {
            var model = Create(entity);
            model.ConsentStatus = consentStatus;
            return model;
        }

        public ConsentGroupModel Create(ConsentGroup consentGroup, int[] acceptedConsentIds, int[] deniedConsentIds)
        {
            var model = Create(consentGroup);
            foreach (var consentModel in model.Consents)
            {
                if (acceptedConsentIds.Contains(consentModel.Id))
                    consentModel.ConsentStatus = ConsentStatus.Accepted;
                else if (deniedConsentIds.Contains(consentModel.Id))
                    consentModel.ConsentStatus = ConsentStatus.Denied;
                else
                    consentModel.ConsentStatus = ConsentStatus.NotSelected;
            }

            return model;
        }

      
    }
}