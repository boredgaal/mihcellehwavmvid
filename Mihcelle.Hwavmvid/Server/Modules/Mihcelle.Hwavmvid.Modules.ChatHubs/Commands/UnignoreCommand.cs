using System.Composition;
using System.Threading.Tasks;
using Oqtane.ChatHubs.Enums;
using Oqtane.ChatHubs.Models;
using Mihcelle.Hwavmvid.Shared.Constants;

namespace Mihcelle.Hwavmvid.Modules.ChatHubs.Commands
{
    [Export("ICommand", typeof(ICommand))]
    [Command("unignore", "[]", new string[] { Authentication.Anonymousrole, Authentication.Userrole, Authentication.Administratorrole, Authentication.Hostrole }, "Usage: /unignore | /unblock")]
    public class UnignoreCommand : BaseCommand
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

            ChatHubIgnore chatHubIgnore = context.ChatHubRepository.GetIgnore(caller.Id, targetUser.Id);
            if (chatHubIgnore != null)
            {
                context.ChatHubRepository.DeleteIgnore(caller.Id, targetUser.Id);
            }

        }
    }
}