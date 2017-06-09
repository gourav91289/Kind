using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OmniPot.Data.Models
{
    public class InventoryConfiguration : EntityBase
    {
        public Guid InventoryConfigurationId { get; set; }
        public Guid TenantId { get; set; }
        [ForeignKey("TenantId")]
        public Tenant Tenant { get; set; }
        public Guid LocationId { get; set; }
        [ForeignKey("LocationId")]
        public Location Location { get; set; }

        //TODO: Inventory settings that apply to this location
    }
}
