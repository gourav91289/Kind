using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OmniPot.Data.Models
{
    public enum MessageType
    {
        None = 0,
        ToConnection = 1,
        ToUsername = 2,
        ToUserId = 3, 
        BroadcastAll = 4, 
        BroadcastTenant = 5, 
        BroadcastLocation = 6,        
    }

    public class AppMessage : EntityBase
    {
        public Guid AppMessageId { get; set; }
        public MessageType MessageType { get; set; }
        public Guid? ConnectionId { get; set; }

        public Guid? UserId { get; set; }

        public string Username { get; set; }
        public string Tenant { get; set; }
        public string Location { get; set; }
        public string Content { get; set; }
    }
}
