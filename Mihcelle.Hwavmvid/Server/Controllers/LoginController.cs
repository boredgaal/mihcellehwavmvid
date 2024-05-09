using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Mihcelle.Hwavmvid.Shared.Models;
using System.Text.Json;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace Mihcelle.Hwavmvid.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Logincontroller : ControllerBase
    {

        public UserManager<Applicationuser> usermanager { get; set; }
        public SignInManager<Applicationuser> signinmanager { get; set; }

        public Logincontroller(UserManager<Applicationuser> usermanager, SignInManager<Applicationuser> signinmanager)
        {
            this.usermanager = usermanager;
            this.signinmanager = signinmanager;
        }

        [AllowAnonymous]
        [HttpGet("{username}/{password}")]
        public async Task Get(string username, string password)
        {
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                var identityuser = await usermanager.FindByNameAsync(username);
                if (identityuser != null)
                {
                    var result = await signinmanager.PasswordSignInAsync(identityuser, password, true, false);
                    if (!result.Succeeded)
                    {
                        throw new HubException("user sign in failed..");
                    }
                }
            }
        }
    }
}
