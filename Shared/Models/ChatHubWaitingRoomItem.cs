using System;

namespace Oqtane.ChatHubs.Models
{
    public class ChatHubWaitingRoomItem
    {

        public Guid Guid { get; set; }

        public string RoomId { get; set; }

        public string UserId { get; set; }

        public string DisplayName { get; set; }

    }
}
