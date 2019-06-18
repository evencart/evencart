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

        string GetString(string url, NameValueCollection data = null);

        Task<string> GetStringAsync(string url, NameValueCollection data = null);
    }
}