using System.Linq;
using RoastedMarketplace.Core.Infrastructure;
using RoastedMarketplace.Data.Entity.Users;
using RoastedMarketplace.Services.Users;

namespace RoastedMarketplace.Infrastructure.Helpers
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
                foreach (var newCapability in providerCapabilityNames.Except(savedCapabilitiesNames))
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