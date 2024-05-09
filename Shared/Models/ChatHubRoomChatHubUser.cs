using System.ComponentModel.DataAnnotations.Schema;

namespace Oqtane.ChatHubs.Models
{
    public class ChatHubRoomChatHubUser : ChatHubBaseModel
    {

        public string ChatHubRoomId { get; set; }
        public string ChatHubUserId { get; set; }

        [NotMapped] public virtual ChatHubUser User { get; set; }
        [NotMapped] public virtual ChatHubRoom Room { get; set; }

    }
}