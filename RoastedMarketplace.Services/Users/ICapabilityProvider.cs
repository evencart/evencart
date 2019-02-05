#region Author Information
// ICapabilityProvider.cs
// 
// (c) 2016 Apexol Technologies. All Rights Reserved.
// 
#endregion

using System.Collections.Generic;

namespace RoastedMarketplace.Services.Users
{
    public interface ICapabilityProvider
    {
        IDictionary<string, IList<string>> GetCapabilities();

        IList<string> GetRawCapabilities();
    }
}