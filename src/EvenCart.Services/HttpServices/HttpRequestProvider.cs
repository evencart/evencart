using System;
using System.Collections.Specialized;
using System.Net;
using System.Threading.Tasks;
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

        private async Task<string> SendProxyRequest(string url, string method, NameValueCollection parameters)
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
                            url += parameters.ToString();
                        }
                        resBytes = await webClient.DownloadDataTaskAsync(url);
                    }
                    else
                    {
                        resBytes = await webClient.UploadValuesTaskAsync(url, method, parameters);
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
        #endregion
    }
}