using EvenCart.Infrastructure.Mvc.Models;

namespace EvenCart.Models.ConnectedAccounts
{
    public class ConnectedAccountModel : FoundationEntityModel
    {
        public string ProviderName { get; set; }
    }
}