using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OmniPot.Data.Models
{
    public class TaxAgency
    {
        public Guid TaxAgencyId { get; set; }
        [StringLength(50)]
        public string DisplayName { get; set; }
        public Guid AddressId { get; set; }
        [ForeignKey("AddressId")]
        public Address MailingAddress { get; set; }
    }
}
