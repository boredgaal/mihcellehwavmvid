using System.Composition;
using System.Threading.Tasks;
using Mihcelle.Hwavmvid.Shared.Constants;
using Microsoft.AspNetCore.SignalR;
using Oqtane.ChatHubs.Models;
using Oqtane.ChatHubs.Enums;
using Mihcelle.Hwavmvid.Modules.ChatHubs.Repository;
using Microsoft.EntityFrameworkCore;

namespace Mihcelle.Hwavmvid.Modules.ChatHubs.Commands
{
    [Export("ICommand", typeof(ICommand))]
    [Command("kick", "[username]", new string[] { Authentication.Administratorrole, Authentication.Hostrole }, "Usage: /kick | /throw | fly")]
    public class KickCommand : ModeratorCommand
    {
        public override async Task ExecuteModeratorOperation(CommandServicesContext context, CommandCallerContext callerContext, string[] args, ChatHubUser caller)
        {

            if (args.Length == 0)
            {
                await context.ChatHub.SendClientNotification("No arguments found.", callerContext.RoomId, callerContext.ConnectionId, caller, ChatHubMessageType.System);
                return;
            }

            string targetUserName = args[0];
            ChatHubUser targetUser = await context.ChatHubRepository.GetUserByUserNameAsync(targetUserName);

            if (targetUser == null)
            {
                await context.ChatHub.SendClientNotification("No user found.", callerContext.RoomId, callerContext.ConnectionId, caller, ChatHubMessageType.System);
                return;
            }

            var targetUserConnections = await context.ChatHubRepository.GetConnectionsByUserId(targetUser.Id).Active().ToListAsync();
            foreach (var connection in targetUserConnections)
            {
                await context.ChatHub.Clients.Client(connection.ConnectionId).SendAsync("Disconnect", context.ChatHubService.CreateChatHubUserClientModel(targetUser));
            }

        }
    }
}