using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Mihcelle.Hwavmvid.Shared.Models;
using Mihcelle.Hwavmvid.Shared;
using System;

namespace Oqtane.ChatHubs.Models
{

    public class ChatHubUser : Applicationuser, IAuditable
    {

        public string FrameworkUserId { get; set; }
        public string DisplayName { get; set; }
        public string UserType { get; set; }

        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }

        [NotMapped] public bool UserlistItemCollapsed { get; set; }
        [NotMapped] public virtual IList<ChatHubRoomChatHubUser> UserRooms { get; set; }
        [NotMapped] public virtual IList<ChatHubConnection> Connections { get; set; }
        [NotMapped] public virtual ChatHubSettings Settings { get; set; }
        [NotMapped] public virtual ICollection<ChatHubIgnore> Ignores { get; set; }
        [NotMapped] public virtual IList<ChatHubInvitation> Invitations { get; set; }
        [NotMapped] public virtual IList<ChatHubDevice> Devices { get; set; }

    }
}