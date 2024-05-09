using Microsoft.AspNetCore.SignalR;
using System.Composition;
using System.Threading.Tasks;
using Mihcelle.Hwavmvid.Shared.Constants;
using System.Linq;
using Mihcelle.Hwavmvid.Modules.ChatHubs.Repository;
using System;
using System.Collections.Generic;
using Oqtane.ChatHubs.Enums;
using Oqtane.ChatHubs.Models;
using Microsoft.EntityFrameworkCore;

namespace Mihcelle.Hwavmvid.Modules.ChatHubs.Commands
{
    [Export("ICommand", typeof(ICommand))]
    [Command("whisper", "[username]", new string[] { Authentication.Userrole, Authentication.Administratorrole, Authentication.Hostrole }, "Usage: /whisper | /asmr")]
    public class WhisperCommand : BaseCommand
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

            if (!targetUser.Online())
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

            ChatHubMessage chatHubMessage = new ChatHubMessage()
            {
                ChatHubRoomId = callerContext.RoomId,
                ChatHubUserId = caller.Id,
                Content = msg ?? string.Empty,
                Type = Enum.GetName(typeof(ChatHubMessageType), ChatHubMessageType.Whisper),
                CreatedBy = caller.UserName,
                CreatedOn = DateTime.Now,
                ModifiedBy = caller.UserName,
                ModifiedOn = DateTime.Now,
            };
            await context.ChatHubRepository.AddMessage(chatHubMessage);
            var chatHubMessageClientModel = context.ChatHubService.CreateChatHubMessageClientModel(chatHubMessage, caller);

            var users = new List<ChatHubUser>(); users.Add(caller); users.Add(targetUser);

            foreach (var item in users)
            {
                var rooms = context.ChatHubRepository.GetRoomsByUser(item).FilterByModuleId(callerContext.ModuleId).Public().Enabled().ToList();
                var connections = await context.ChatHubRepository.GetConnectionsByUserId(item.Id).Active().ToListAsync();

                foreach (var room in rooms)
                {
                    foreach (var connection in connections)
                    {
                        await context.ChatHub.Clients.Client(connection.ConnectionId).SendAsync("AddMessage", chatHubMessageClientModel);
                    }
                }
            }

        }
    }
}