using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace TechTalk.FeatureToggles.Auth
{
    public class HeaderAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly IHttpContextAccessor httpContextAssessor;

        public HeaderAuthHandler(IHttpContextAccessor httpContextAssessor, IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock):
            base(options, logger, encoder, clock)
        {
            this.httpContextAssessor = httpContextAssessor;
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (this.httpContextAssessor.HttpContext.Request.Headers.ContainsKey("User"))
            {
                var identity = new ClaimsIdentity(new[] {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, this.httpContextAssessor.HttpContext.Request.Headers["User"])
                });

                return Task.FromResult(AuthenticateResult.Success(new AuthenticationTicket(new ClaimsPrincipal(identity), "User")));
            }

            return Task.FromResult(AuthenticateResult.Fail("No User header"));
        }
    }
}