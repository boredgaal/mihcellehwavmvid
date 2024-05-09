using System.ComponentModel.DataAnnotations.Schema;

namespace Oqtane.ChatHubs.Models
{
    public class ChatHubRoomChatHubWhitelistUser : ChatHubBaseModel
    {
        public string ChatHubRoomId { get; set; }
        public string ChatHubWhitelistUserId { get; set; }


        [NotMapped] public virtual ChatHubRoom Room { get; set; }
        [NotMapped] public virtual ChatHubWhitelistUser WhitelistUser { get; set; }
    }
}
