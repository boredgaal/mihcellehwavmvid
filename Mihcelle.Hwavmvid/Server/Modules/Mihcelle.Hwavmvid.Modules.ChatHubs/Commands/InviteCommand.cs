using Microsoft.AspNetCore.SignalR;
using System.Composition;
using System.Threading.Tasks;
using Mihcelle.Hwavmvid.Shared.Constants;
using System;
using System.Linq;
using Mihcelle.Hwavmvid.Modules.ChatHubs.Repository;
using Oqtane.ChatHubs.Models;
using Oqtane.ChatHubs.Enums;
using Microsoft.EntityFrameworkCore;

namespace Mihcelle.Hwavmvid.Modules.ChatHubs.Commands
{
    [Export("ICommand", typeof(ICommand))]
    [Command("invite", "[username]", new string[] { Authentication.Userrole, Authentication.Administratorrole, Authentication.Hostrole } , "Usage: /invite")]
    public class InviteCommand : BaseCommand
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

            var targetUserConnections = await context.ChatHubRepository.GetConnectionsByUserId(targetUser.Id).Active().ToListAsync();
            if (!targetUserConnections.Any())
            {
                await context.ChatHub.SendClientNotification("User not online.", callerContext.RoomId, callerContext.ConnectionId, caller, ChatHubMessageType.System);
                return;
            }

            if (caller.Id == targetUser.Id)
            {
                await context.ChatHub.SendClientNotification("Calling user can not be target user.", callerContext.RoomId, callerContext.ConnectionId, caller, ChatHubMessageType.System);
                return;
            }

            string msg = null;
            if (args.Length > 1)
            {
                msg = String.Join(" ", args.Skip(1)).Trim();
            }

            var callerRoom = await context.ChatHubRepository.GetRoomById(callerContext.RoomId);
            var oneVsOneRoom = await context.ChatHubService.GetOneVsOneRoomAsync(caller, targetUser, callerRoom.ModuleId);

            if(oneVsOneRoom != null)
            {
                await context.ChatHub.EnterChatRoom(oneVsOneRoom.Id);

                ChatHubInvitation chatHubInvitation = new ChatHubInvitation()
                {
                    ChatHubUserId = targetUser.Id,
                    RoomId = oneVsOneRoom.Id,
                    Hostname = caller.DisplayName,
                    CreatedBy = targetUser.UserName,
                    CreatedOn = DateTime.Now,
                    ModifiedBy = targetUser.UserName,
                    ModifiedOn = DateTime.Now,
                };

                context.ChatHubRepository.AddInvitation(chatHubInvitation);

                /*
                var targetUserConnections = await context.ChatHubRepository.GetConnectionsByUserId(targetUser.UserId).Active().ToListAsync();
                foreach (var connection in targetUserConnections)
                {
                    await context.ChatHub.Clients.Client(connection.ConnectionId).SendAsync("AddInvitation", chatHubInvitation);
                }
                */
            }

        }
    }
}