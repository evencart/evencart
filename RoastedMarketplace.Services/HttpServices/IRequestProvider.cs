using System.Collections.Specialized;
using System.Threading.Tasks;

namespace RoastedMarketplace.Services.HttpServices
{
    public interface IRequestProvider
    {
        T Get<T>(string url);

        Task<T> GetAsync<T>(string url);

        T Post<T>(string url, NameValueCollection data = null);

        Task<T> PostAsync<T>(string url, NameValueCollection data = null);

        string GetString(string url);

        Task<string> GetStringAsync(string url);
    }
}