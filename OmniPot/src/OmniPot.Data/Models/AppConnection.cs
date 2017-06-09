using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OmniPot.Data.Models
{
    public class AppConnection : EntityBase
    {
        public Guid AppConnectionId { get; set; }
        
        public string Tenant { get; set; }

        public string Location { get; set; }

        public Guid ConnectionId { get; set; }

        public string Username { get; set; }

        public Guid UserId { get; set; }
    }
}
