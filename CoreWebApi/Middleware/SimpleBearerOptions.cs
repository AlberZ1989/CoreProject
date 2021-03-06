using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;

namespace CoreWebApi.Middleware
{
    public class SimpleBearerOptions : AuthenticationOptions, IOptions<SimpleBearerOptions>
    {
        public SimpleBearerOptions()
        {
            AuthenticationScheme = "Bearer";
            AutomaticAuthenticate = false;
        }

        public IDictionary<string, ClaimsPrincipal> IdentityMap { get; set; }

        public SimpleBearerOptions Value
        {
            get
            {
                return this;
            }
        }
    }
}
    