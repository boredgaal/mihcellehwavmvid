using System.Composition;
using System.Threading.Tasks;
using Oqtane.ChatHubs.Models;
using Mihcelle.Hwavmvid.Shared.Constants;

namespace Mihcelle.Hwavmvid.Modules.ChatHubs.Commands
{
    [Export("ICommand", typeof(ICommand))]
    [Command("commands", "[]", new string[] { Authentication.Anonymousrole, Authentication.Userrole, Authentication.Administratorrole, Authentication.Hostrole }, "Usage: /commands | /list-commands")]
    public class ListCommandsCommand : BaseCommand
    {

        public override async Task Execute(CommandServicesContext context, CommandCallerContext callerContext, string[] args, ChatHubUser caller)
        {

            await context.ChatHub.SendCommandMetaDatas(callerContext.RoomId);            

        }

    }
}