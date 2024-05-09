using System.ComponentModel.DataAnnotations.Schema;

namespace Oqtane.ChatHubs.Models
{
    public class ChatHubRoomChatHubModerator : ChatHubBaseModel
    {

        public string ChatHubRoomId { get; set; }
        public string ChatHubModeratorId { get; set; }


        [NotMapped] public virtual ChatHubRoom Room { get; set; }
        [NotMapped] public virtual ChatHubModerator Moderator { get; set; }

    }
}
