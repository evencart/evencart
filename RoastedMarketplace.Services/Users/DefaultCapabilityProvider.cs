#region Author Information
// CapabilityProvider.cs
// 
// (c) 2016 Apexol Technologies. All Rights Reserved.
// 
#endregion

using System.Collections.Generic;
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
                        CapabilitySystemNames.ManageUsers,
                        CapabilitySystemNames.ManageEmailAccounts,
                        CapabilitySystemNames.CommentDelete,
                    }
                }
            };
        }
    }
}