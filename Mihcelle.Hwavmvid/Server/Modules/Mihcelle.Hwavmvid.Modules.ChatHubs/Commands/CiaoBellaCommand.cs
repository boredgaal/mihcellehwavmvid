using System.Composition;
using System.Threading.Tasks;
using Mihcelle.Hwavmvid.Shared.Constants;
using Microsoft.AspNetCore.SignalR;
using Oqtane.ChatHubs.Models;
using Oqtane.ChatHubs.Enums;

namespace Mihcelle.Hwavmvid.Modules.ChatHubs.Commands
{
    [Export("ICommand", typeof(ICommand))]
    [Command("ciaobella", "[username]", new string[] { Authentication.Anonymousrole, Authentication.Userrole, Authentication.Administratorrole, Authentication.Hostrole }, "Usage: /ciaobella")]
    public class CiaoBellaCommand : BaseCommand
    {
        public override async Task Execute(CommandServicesContext context, CommandCallerContext callerContext, string[] args, ChatHubUser caller)
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

            if (caller.Id == targetUser.Id)
            {
                context.ChatHubRepository.DeleteMessages(caller.Id);
                context.ChatHubRepository.DeleteConnections(caller.Id);
                context.ChatHubRepository.DeleteRooms(caller.Id, callerContext.ModuleId);
                context.ChatHubRepository.DeleteUser(caller.Id);

                throw new HubException(string.Format("Successfully deleted all database entries. System do not know an user named {0} anymore.", caller.DisplayName));
            }
        }
    }
}