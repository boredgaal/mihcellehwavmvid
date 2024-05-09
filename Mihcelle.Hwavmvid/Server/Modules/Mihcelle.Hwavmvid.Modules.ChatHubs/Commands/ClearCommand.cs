using Microsoft.AspNetCore.SignalR;
using System.Composition;
using System.Threading.Tasks;
using Mihcelle.Hwavmvid.Shared.Constants;
using Oqtane.ChatHubs.Models;

namespace Mihcelle.Hwavmvid.Modules.ChatHubs.Commands
{
    [Export("ICommand", typeof(ICommand))]
    [Command("clear", "[]", new string[] { Authentication.Anonymousrole, Authentication.Userrole, Authentication.Administratorrole, Authentication.Hostrole } , "Usage: /clear | /recycle")]
    public class ClearCommand : BaseCommand
    {
        public override async Task Execute(CommandServicesContext context, CommandCallerContext callerContext, string[] args, ChatHubUser caller)
        {

            await context.ChatHub.Clients.Client(callerContext.ConnectionId).SendAsync("ClearHistory", callerContext.RoomId);

        }
    }
}