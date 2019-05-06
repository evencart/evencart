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