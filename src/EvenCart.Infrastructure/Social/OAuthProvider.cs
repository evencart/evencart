using System.Collections.Generic;

namespace EvenCart.Infrastructure.Social
{
    public class OAuthProvider
    {
        public string ClientId { get; set; }

        public string ClientSecret { get; set; }

        public string AuthorizeUrl { get; set; }

        public Dictionary<string, string> AuthorizeAdditionalParameters { get; set; }

        public string RedirectUrl { get; set; }

        public string TokenUrl { get; set; }

        public string Scope { get; set; }

        public Dictionary<string, string> Keys { get;  } = new Dictionary<string, string>()
        {
            { nameof(ClientId), "client_id" },
            { nameof(ClientSecret), "client_secret" },
            { nameof(RedirectUrl), "redirect_uri" }
        };

        public string Authorize()
        {
            
        }
    }
}