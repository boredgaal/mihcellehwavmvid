using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Mihcelle.Hwavmvid.Shared.Constants;
using Mihcelle.Hwavmvid.Shared.Models;

namespace Mihcelle.Hwavmvid.Server.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class Registrationcontroller : ControllerBase
    {
        public UserManager<Applicationuser> usermanager { get; set; }
        public SignInManager<Applicationuser> signinmanager { get; set; }
        public RoleManager<IdentityRole> rolemanager { get; set; }

        public Registrationcontroller(UserManager<Applicationuser> usermanager, SignInManager<Applicationuser> signinmanager, RoleManager<IdentityRole> rolemanager)
        {
            this.usermanager = usermanager;
            this.signinmanager = signinmanager;
            this.rolemanager = rolemanager;
        }

        [HttpGet("{username}/{email}/{password}")]
        [AllowAnonymous]
        public async Task Get(string username, string email, string password)
        {

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                throw new HubException("no valid data provided");
            }

            var applicationuser = new Applicationuser();
            applicationuser.UserName = username;
            applicationuser.Email = email;
            applicationuser.PasswordHash = password;
            applicationuser.EmailConfirmed = true;
            applicationuser.TwoFactorEnabled = false;
            applicationuser.LockoutEnabled = true;

            var createuserresult = await this.usermanager.CreateAsync(applicationuser, password);
            if (createuserresult.Succeeded)
            {
                if (!await this.rolemanager.RoleExistsAsync(Authentication.Userrole))
                {
                    await this.rolemanager.CreateAsync(new IdentityRole(Authentication.Userrole));
                }

                var addtoroleresult = await usermanager.AddToRoleAsync(applicationuser, Authentication.Userrole);
                if (!addtoroleresult.Succeeded)
                {
                    throw new HubException("Failed to add user to role..");
                }
            }
        }
    }
}
