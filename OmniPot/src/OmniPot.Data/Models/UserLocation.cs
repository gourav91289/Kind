using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OmniPot.Data.Models
{
    public class UserLocation : EntityBase
    {
        public Guid UserLocationId { get; set; }
        public Guid UserId { get; set; }
        [ForeignKey("LocationId")]
        public Guid LocationId { get; set; }
        public Location Location { get; set; }

        [ForeignKey("TenantId")]
        public Guid TenantId { get; set; }
        public Tenant Tenant { get; set; }
    }
}
