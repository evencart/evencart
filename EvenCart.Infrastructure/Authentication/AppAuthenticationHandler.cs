using System.Text.Encodings.Web;
using System.Threading.Tasks;
using EvenCart.Services.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace EvenCart.Infrastructure.Authentication
{
    public class AppAuthenticationHandler : CookieAuthenticationHandler
    {
        private readonly IAppAuthenticationService _appAuthenticationService;
        public AppAuthenticationHandler(IOptionsMonitor<AppAuthenticationOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock, IAppAuthenticationService appAuthenticationService) : base(options, logger, encoder, clock)
        {
            _appAuthenticationService = appAuthenticationService;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var result = await base.HandleAuthenticateAsync();
            return result;
        }
    }
}