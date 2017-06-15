using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OmniPot.Data.Models
{
    public class LocationType : EntityBase
    {
        public Guid LocationTypeId { get; set; } 
        [StringLength(100)]
        public String LocationTypeName { get; set; }
        [ForeignKey("TenantId")]
        public Guid TenantId { get; set; }
        public Tenant Tenant { get; set; }
        public virtual List<Location> Locations { get; set; }
        public Guid LocationTypeId { get; set; }

        [StringLength(50)]
        public string LocationTypeName { get; set; }

        public Guid TenantId { get; set; }

        [ForeignKey("TenantId")]
        public Tenant Tenant { get; set; }       
    }
}
