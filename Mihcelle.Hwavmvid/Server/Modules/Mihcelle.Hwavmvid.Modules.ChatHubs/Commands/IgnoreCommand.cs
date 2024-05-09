using System.Composition;
using System.Threading.Tasks;
using Mihcelle.Hwavmvid.Shared.Constants;
using Oqtane.ChatHubs.Models;
using Oqtane.ChatHubs.Enums;

namespace Mihcelle.Hwavmvid.Modules.ChatHubs.Commands
{
    [Export("ICommand", typeof(ICommand))]
    [Command("ignore", "[username]", new string[] { Authentication.Anonymousrole, Authentication.Userrole, Authentication.Administratorrole, Authentication.Hostrole }, "Usage: /ignore | /block")]
    public class IgnoreCommand : BaseCommand
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

            await context.ChatHubService.IgnoreUser(caller, targetUser);
            await context.ChatHub.SendClientNotification(string.Concat(targetUser.DisplayName, " ", "has been added to ignore list."), callerContext.RoomId, callerContext.ConnectionId, caller, ChatHubMessageType.System);

        }
    }
}