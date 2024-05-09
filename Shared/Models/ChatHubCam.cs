using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Oqtane.ChatHubs.Models
{
    public class ChatHubCam : ChatHubBaseModel
    {

        public string ChatHubRoomId { get; set; }
        public string ChatHubConnectionId { get; set; }

        public string Status { get; set; }

        public string VideoUrl { get; set; }
        public string VideoUrlExtension { get; set; }

        [NotMapped] public virtual ChatHubRoom Room { get; set; }
        [NotMapped] public virtual ChatHubConnection Connection { get; set; }
        [NotMapped] public virtual List<ChatHubCamSequence> Sequences { get; set; }
        [NotMapped] public int TotalVideoSequences { get; set; }

    }
}
