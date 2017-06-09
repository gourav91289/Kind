using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OmniPot.Data.Models
{
    public class TaxItem : EntityBase
    {
        public Guid TaxItemId {get;set;}

        [Required]
        public Guid TenantId { get; set; }
        //[ForeignKey("TenantId")]
       // public Tenant Tenant { get; set; }
        [StringLength(50)]
        public string DisplayName { get; set; }
        //public Guid TaxAgencyId { get; set; }
        //[ForeignKey("TaxAgencyId")]
        //public TaxAgency Agency { get; set; }

        public string AgencyName { get; set; }
        public decimal Rate { get; set; }

        public IList<TaxGroupItem> TaxGroupItems { get; set; } = new List<TaxGroupItem>();
    }
}
