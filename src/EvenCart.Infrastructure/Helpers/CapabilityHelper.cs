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
using EvenCart.Core.Infrastructure;
using EvenCart.Data.Entity.Users;
using EvenCart.Services.Users;

namespace EvenCart.Infrastructure.Helpers
{
    public static class CapabilityHelper
    {
        public static void UpgradeCapabilities()
        {
            var capabilityProviders = DependencyResolver.ResolveMany<ICapabilityProvider>();
            var capabilityService = DependencyResolver.Resolve<ICapabilityService>();
            var savedCapabilitiesNames = capabilityService.Get(x => true).Select(x => x.Name).ToList();
            foreach (var cp in capabilityProviders)
            {
                var providerCapabilityNames = cp.GetRawCapabilities();
                foreach (var newCapability in providerCapabilityNames.Except(savedCapabilitiesNames, StringComparer.InvariantCultureIgnoreCase))
                {
                    capabilityService.Insert(new Capability()
                    {
                        Name = newCapability,
                        IsActive = true
                    });
                }
            }

        }   
    }
}