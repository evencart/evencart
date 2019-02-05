#region Author Information
// CapabilityProvider.cs
// 
// (c) 2016 Apexol Technologies. All Rights Reserved.
// 
#endregion

using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using RoastedMarketplace.Data.Constants;
using RoastedMarketplace.Data.Entity.Users;

namespace RoastedMarketplace.Services.Users
{
    public class DefaultCapabilityProvider : ICapabilityProvider
    {
        private IDictionary<string, IList<string>> _defaultCapabilities;

        public IDictionary<string, IList<string>> GetCapabilities()
        {
            FillDefaultCapabilities();
            return _defaultCapabilities;
        }

        public IList<string> GetRawCapabilities()
        {
            var allCapabilityFields = typeof(CapabilitySystemNames).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);
            return allCapabilityFields.Select(x => (string)x.GetRawConstantValue()).ToList();
        }

        private void FillDefaultCapabilities()
        {
            if (_defaultCapabilities != null)
                return;
            _defaultCapabilities = new Dictionary<string, IList<string>>()
            {
                //admins have all capabilities by default so no need to specify them separately
                {
                    SystemRoleNames.Manager, new List<string>()
                    {
                        CapabilitySystemNames.EditProduct,
                        CapabilitySystemNames.ViewUsers,
                        CapabilitySystemNames.ManageEmailAccounts,
                    }
                }
            };
        }
    }
}