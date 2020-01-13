using EvenCart.Data.Entity.Social;

namespace EvenCart.Infrastructure.Social
{
    public interface IConnectionAccountant
    {
        bool Connect(ConnectedAccountRequest request);

        bool IsConnected(string providerName, string providerUserId);
    }
}