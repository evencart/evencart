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