using System.Composition;
using System.Threading.Tasks;
using Oqtane.ChatHubs.Models;
using Oqtane.ChatHubs.Enums;
using Mihcelle.Hwavmvid.Shared.Constants;

namespace Mihcelle.Hwavmvid.Modules.ChatHubs.Commands
{
    [Export("ICommand", typeof(ICommand))]
    [Command("blacklistuser", "[username]", new string[] { Authentication.Anonymousrole, Authentication.Userrole, Authentication.Administratorrole, Authentication.Hostrole }, "Usage: /blacklistuser")]
    public class BlacklistUserCommand : BaseCommand
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

            await context.ChatHub.AddBlacklistUser(targetUser.Id, callerContext.RoomId);
        }
    }
}
