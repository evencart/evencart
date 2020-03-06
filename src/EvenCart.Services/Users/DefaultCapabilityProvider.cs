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

using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using EvenCart.Data.Constants;
using EvenCart.Data.Entity.Users;

namespace EvenCart.Services.Users
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