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

using System.Collections.Specialized;
using System.Threading.Tasks;

namespace EvenCart.Services.HttpServices
{
    public interface IRequestProvider
    {
        T Get<T>(string url, NameValueCollection data = null);

        Task<T> GetAsync<T>(string url, NameValueCollection data = null);

        T Post<T>(string url, NameValueCollection data = null);

        Task<T> PostAsync<T>(string url, NameValueCollection data = null);

        T Post<T>(string url, object data);
        
        Task<T> PostAsync<T>(string url, object data);
        
        string GetString(string url, NameValueCollection data = null);

        Task<string> GetStringAsync(string url, NameValueCollection data = null);
    }
}