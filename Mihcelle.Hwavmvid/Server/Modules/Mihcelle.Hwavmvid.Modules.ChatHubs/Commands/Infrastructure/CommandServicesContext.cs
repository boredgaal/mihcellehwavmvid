using Microsoft.AspNetCore.Identity;
using Mihcelle.Hwavmvid.Modules.ChatHubs.Hubs;
using Mihcelle.Hwavmvid.Modules.ChatHubs.Repository;
using Mihcelle.Hwavmvid.Modules.ChatHubs.Providers;
using Mihcelle.Hwavmvid.Shared.Models;

namespace Mihcelle.Hwavmvid.Modules.ChatHubs.Commands
{
    public class CommandServicesContext
    {
        public ChatHub ChatHub { get; set; }
        public ChatHubRepository ChatHubRepository { get; set; }
        public ChatHubService ChatHubService { get; set; }
        public UserManager<Applicationuser> UserManager { get; set; }
    }
}