using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Oqtane.ChatHubs.Models
{
    public class ChatHubIgnoredBy
    {

        public string Id { get; set; }
        public string ChatHubUserId { get; set; }
        public string ChatHubIgnoredUserId { get; set; }

        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }

        [NotMapped]
        public virtual ChatHubUser User { get; set; }

    }
}