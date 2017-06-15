using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using System.Linq;
using OmniPot.Data.Identity;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OmniPot.Data.Models
{
    public class UserLocation : EntityBase
    {
        public Guid UserLocationId { get; set; }
       
        public Guid LocationId { get; set; }

        [ForeignKey("LocationId")]
        public Location Location { get; set; }
       
        public Guid TenantId { get; set; }

        [ForeignKey("TenantId")]
        public Tenant Tenant { get; set; }

        public Guid Id { get; set; }

        [ForeignKey("Id")]
        public ApplicationUser User { get; set; }

    }
}
