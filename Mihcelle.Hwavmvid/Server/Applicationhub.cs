using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Mihcelle.Hwavmvid.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mihcelle.Hwavmvid.Shared.Constants;

namespace Mihcelle.Hwavmvid.Server
{
    public class Applicationhub : Hub
    {

        public UserManager<Applicationuser> usermanager { get; set; }
        public SignInManager<Applicationuser> signinmanager { get; set; }
        public RoleManager<IdentityRole> rolemanager { get; set; }

        public Applicationhub(UserManager<Applicationuser> usermanager, SignInManager<Applicationuser> signinmanager, RoleManager<IdentityRole> rolemanager)
        {
            this.usermanager = usermanager;
            this.signinmanager = signinmanager;
            this.rolemanager = rolemanager;
        }

        [AllowAnonymous]
        public override async Task OnConnectedAsync()
        {
            Console.WriteLine("-------------------------------------");
            Console.WriteLine("connected to application hub..");
            Console.WriteLine("-------------------------------------");
            
            await base.OnConnectedAsync();
        }

        [AllowAnonymous]
        public async Task Establishapplicationconnection()
        {

        }

        [AllowAnonymous]
        public override Task OnDisconnectedAsync(Exception? exception)
        {
            return base.OnDisconnectedAsync(exception);
        }
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
        public object? GetService(Type serviceType)
        {
            throw new NotImplementedException();
        }

    }
}
