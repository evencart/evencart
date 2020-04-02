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
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using EvenCart.Services.Serializers;

namespace EvenCart.Services.HttpServices
{
    public class HttpRequestProvider : IRequestProvider
    {
        private readonly IDataSerializer _dataSerializer;
        public HttpRequestProvider(IDataSerializer dataSerializer)
        {
            _dataSerializer = dataSerializer;
        }

        public T Get<T>(string url, NameValueCollection data = null)
        {
            return GetAsync<T>(url, data).Result;
        }

        public async Task<T> GetAsync<T>(string url, NameValueCollection data = null)
        {
            var response = await GetStringAsync(url, data);
            return _dataSerializer.DeserializeAs<T>(response);
        }

        public T Post<T>(string url, NameValueCollection data = null)
        {
            return PostAsync<T>(url, data).Result;
        }

        public async Task<T> PostAsync<T>(string url, NameValueCollection data = null)
        {
            var response = await SendProxyRequest(url, "POST", data);
            return _dataSerializer.DeserializeAs<T>(response);
        }

        public T Post<T>(string url, object data)
        {
            return PostAsync<T>(url, data).Result;
        }

        public async Task<T> PostAsync<T>(string url, object data)
        {
            var response = await SendProxyRequest(url, "POST", data);
            return _dataSerializer.DeserializeAs<T>(response);
        }

        public string GetString(string url, NameValueCollection data = null)
        {
            return GetStringAsync(url, data).Result;
        }

        public async Task<string> GetStringAsync(string url, NameValueCollection data = null)
        {
            var response = await SendProxyRequest(url, "GET", data);
            return response;
        }

        #region Helpers

        private async Task<string> SendProxyRequest(string url, string method, object parameters)
        {
            using (var webClient = new WebClient())
            {
                try
                {
                    byte[] resBytes;
                    if (method == "GET")
                    {
                        if (parameters != null)
                        {
                            //add parameters
                            if (!url.Contains('?'))
                                url += "?";
                            if (parameters is NameValueCollection collection)
                            {
                                url += ToQueryString(collection);
                            }
                            else
                                url += parameters.ToString();
                        }
                        resBytes = await webClient.DownloadDataTaskAsync(url);
                    }
                    else
                    {
                        if (parameters is NameValueCollection collection)
                            resBytes = await webClient.UploadValuesTaskAsync(url, method, collection);
                        else
                        {
                            var data = "";
                            if (parameters != null)
                                data = _dataSerializer.Serialize(parameters);
                            webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
                            return await webClient.UploadStringTaskAsync(url, method, data);

                        }

                    }
                    var result = System.Text.Encoding.UTF8.GetString(resBytes);
                    return result;
                }
                catch (Exception e)
                {

                    return null;
                }
            }

        }

        public static string ToQueryString(NameValueCollection collection)
        {
            if (collection == null) return string.Empty;

            var sb = new StringBuilder();

            foreach (string key in collection.Keys)
            {
                if (string.IsNullOrWhiteSpace(key)) continue;

                string[] values = collection.GetValues(key);
                if (values == null) continue;

                foreach (string value in values)
                {
                    sb.AppendFormat("{0}={1}", Uri.EscapeDataString(key), Uri.EscapeDataString(value));
                    sb.Append("&");
                }
            }

            return sb.ToString();
        }
        #endregion
    }
}