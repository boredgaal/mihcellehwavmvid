using System.Composition;
using System.Threading.Tasks;
using Oqtane.ChatHubs.Enums;
using Oqtane.ChatHubs.Models;
using Mihcelle.Hwavmvid.Modules.ChatHubs.Repository;
using Mihcelle.Hwavmvid.Shared.Constants;

namespace Mihcelle.Hwavmvid.Modules.ChatHubs.Commands
{
    [Export("ICommand", typeof(ICommand))]
    [Command("message-color", "[]", new string[] { Authentication.Anonymousrole, Authentication.Userrole, Authentication.Administratorrole, Authentication.Hostrole } , "Usage: /message-color")]
    public class MessageColorCommand : BaseCommand
    {
        public override async Task Execute(CommandServicesContext context, CommandCallerContext callerContext, string[] args, ChatHubUser caller)
        {

            if (args.Length == 0)
            {
                await context.ChatHub.SendClientNotification("No arguments found.", callerContext.RoomId, callerContext.ConnectionId, caller, ChatHubMessageType.System);
                return;
            }

            string messageColor = args[0];

            if(!string.IsNullOrEmpty(messageColor))
            {
                var settings = context.ChatHubRepository.GetSetting(callerContext.UserId);                
                settings.MessageColor = messageColor;
                await context.ChatHubRepository.UpdateSetting(settings);

                await context.ChatHub.SendClientNotification("Message Color Updated.", callerContext.RoomId, callerContext.ConnectionId, caller, ChatHubMessageType.System);
            }

        }
    }
}