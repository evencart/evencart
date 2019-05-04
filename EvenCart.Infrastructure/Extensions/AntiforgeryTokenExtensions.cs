using EvenCart.Infrastructure.Helpers;
using Microsoft.AspNetCore.Antiforgery;

namespace EvenCart.Infrastructure.Extensions
{
    public static class AntiforgeryTokenExtensions
    {
        public static string GetToken(this IAntiforgery antiforgery)
        {
            var token = antiforgery.GetAndStoreTokens(ApplicationEngine.CurrentHttpContext);
            return token.RequestToken;
        }
    }
}