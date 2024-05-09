using System.ComponentModel.DataAnnotations.Schema;

namespace Oqtane.ChatHubs.Models
{
    public class ChatHubDevice : ChatHubBaseModel
    {

        public string ChatHubUserId { get; set; }
        public string ChatHubRoomId { get; set; }
        public string UserAgent { get; set; }
        public string Type { get; set; }
        public string DefaultDeviceId { get; set; }
        public string DefaultDeviceName { get; set; }

        [NotMapped] public virtual ChatHubUser User { get; set; }
        [NotMapped] public virtual ChatHubRoom Room { get; set; }

    }
}