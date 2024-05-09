using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Oqtane.ChatHubs.Models
{
    public class ChatHubMessage : ChatHubBaseModel
    {

        public string ChatHubRoomId { get; set; }
        public string ChatHubUserId { get; set; }
        public string Content { get; set; }
        public string Type { get; set; }

        [NotMapped] public virtual ChatHubRoom Room { get; set; }
        [NotMapped] public virtual ChatHubUser User { get; set; }
        [NotMapped] public virtual IList<ChatHubPhoto> Photos { get; set; }
        [NotMapped] public virtual IList<ChatHubCommandMetaData> CommandMetaDatas { get; set; }

    }
}