using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;
using System.Security.Claims;

namespace Mihcelle.Hwavmvid.Client
{
    public class Applicationauthenticationstateprovider : AuthenticationStateProvider
    {


        private IHttpClientFactory _httpclientfactory { get; set; }
        public Applicationauthenticationstateprovider(IHttpClientFactory httpclientfactory)
        {
            this._httpclientfactory = httpclientfactory;
        }

        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {

            try
            {

                var client = this._httpclientfactory.CreateClient("Mihcelle.Hwavmvid.ServerApi.Unauthenticated");
                var claimsdictitems = await client.GetFromJsonAsync<List<KeyValuePair<string, string>>>("Applicationauthenticationstate");
                var authclaims = new List<Claim>();

                if (claimsdictitems != null && claimsdictitems.Any())
                {
                    foreach (var dicitem in claimsdictitems)
                    {
                        authclaims.Add(new Claim(dicitem.Key, dicitem.Value));
                    }

                    var authenticationstate = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(authclaims, "mihcelle.hwavmvid")));
                    return authenticationstate;
                }
            }
            catch(Exception exception)
            {
                Console.WriteLine(exception.Message);
            }

            var anonymousid = new ClaimsIdentity();
            return new AuthenticationState(new ClaimsPrincipal(anonymousid));
        }

    }
}
