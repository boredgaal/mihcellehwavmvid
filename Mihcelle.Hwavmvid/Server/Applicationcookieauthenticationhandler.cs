using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace Mihcelle.Hwavmvid.Server
{
    public class Applicationcookieauthenticationhandler : CookieAuthenticationHandler
    {

        public Applicationcookieauthenticationhandler(IOptionsMonitor<CookieAuthenticationOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
        {
        }

        public override async Task SignInAsync(ClaimsPrincipal user, AuthenticationProperties? properties)
        {
            try { await base.SignInAsync(user, properties); } catch { }
        }
        protected override async Task HandleSignInAsync(ClaimsPrincipal user, AuthenticationProperties? properties)
        {
            try { await base.HandleSignInAsync(user, properties); } catch { }
        }
        protected override async Task HandleChallengeAsync(AuthenticationProperties properties)
        {
            try { await base.HandleChallengeAsync(properties); } catch { }
        }
        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            try { return await base.HandleAuthenticateAsync(); } catch { return await Task.FromResult(AuthenticateResult.Fail("authentication failed")); }
        }
        protected override async Task InitializeHandlerAsync()
        {
            try { await base.InitializeHandlerAsync(); } catch { }
        }
        protected override string? ResolveTarget(string? scheme)
        {
            try { return base.ResolveTarget(scheme); } catch { return null; }
        }
        protected override async Task InitializeEventsAsync()
        {
            try { await base.InitializeEventsAsync(); } catch { }
        }

    }
}
