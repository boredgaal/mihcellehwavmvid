using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Oqtane.ChatHubs.Models
{
    public class ChatHubInvitation : ChatHubBaseModel
    {

        public string RoomId { get; set; }
        public string Hostname { get; set; }

        public string ChatHubUserId { get; set; }
        [NotMapped] public virtual ChatHubUser User { get; set; }

    }
}
