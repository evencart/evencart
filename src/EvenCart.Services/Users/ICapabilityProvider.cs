using System.Collections.Generic;

namespace EvenCart.Services.Users
{
    public interface ICapabilityProvider
    {
        IDictionary<string, IList<string>> GetCapabilities();

        IList<string> GetRawCapabilities();
    }
}