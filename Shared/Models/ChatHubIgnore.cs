using System.ComponentModel.DataAnnotations.Schema;

namespace Oqtane.ChatHubs.Models
{
    public class ChatHubIgnore : ChatHubBaseModel
    {

        public string ChatHubUserId { get; set; }
        public string ChatHubIgnoredUserId { get; set; }


        [NotMapped]
        public virtual ChatHubUser User { get; set; }

    }
}