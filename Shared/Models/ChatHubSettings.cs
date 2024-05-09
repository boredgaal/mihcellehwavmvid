using System.ComponentModel.DataAnnotations.Schema;

namespace Oqtane.ChatHubs.Models
{
    public class ChatHubSettings : ChatHubBaseModel
    {

        public string UsernameColor { get; set; }
        public string MessageColor { get; set; }

        public string ChatHubUserId { get; set; }
        [NotMapped] public virtual ChatHubUser User { get; set; }

    }
}
