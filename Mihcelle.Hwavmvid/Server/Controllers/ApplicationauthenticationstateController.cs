using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Mihcelle.Hwavmvid.Shared.Models;
using System.Security.Claims;
using System.Text.Json;
using System.Text.Json.Serialization;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace Mihcelle.Hwavmvid.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Applicationauthenticationstatecontroller : ControllerBase
    {

        public UserManager<Applicationuser> usermanager { get; set; }
        public SignInManager<Applicationuser> signinmanager { get; set; }
        public RoleManager<IdentityRole> rolemanager { get; set; }

        public Applicationauthenticationstatecontroller(UserManager<Applicationuser> usermanager, SignInManager<Applicationuser> signinmanager, RoleManager<IdentityRole> rolemanager)
        {
            this.usermanager = usermanager;
            this.signinmanager = signinmanager;
            this.rolemanager = rolemanager;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<List<KeyValuePair<string, string>>?> Get()
        {

            if (User.Identity != null && !string.IsNullOrEmpty(User.Identity.Name) && User.Identity.IsAuthenticated)
            {
                var identityuser = await usermanager.FindByNameAsync(User.Identity.Name);
                if (identityuser != null)
                {
                    ClaimsIdentity claimsidentity = new ClaimsIdentity(HttpContext.User.Claims.ToList(), "requireauthenticateduser");
                    var claimsprincipal = new ClaimsPrincipal(claimsidentity);
                    var authenticationstate = new AuthenticationState(claimsprincipal);

                    List<KeyValuePair<string, string>> claimslist = new();
                    foreach (var claim in HttpContext.User.Claims)
                    {
                        claimslist.Add(new KeyValuePair<string, string>(claim.Type, claim.Value));
                    }

                    return claimslist;                    
                }
            }

            return null;
        }

    }
}
