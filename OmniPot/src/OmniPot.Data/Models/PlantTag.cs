using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OmniPot.Data.Models
{
    public class PlantTag : EntityBase
    {
        [Key]
        public Guid PlantTagId { get; set; }
        public Guid ParentRfidTagId { get; set; }
        [ForeignKey("ParentRfidTagId")]
        public RfidTag ParentRfidTag { get; set; }
        public Guid? AddressId { get; set; }
        [ForeignKey("AddressId")]
        //plant address should be the same for tags owned by one patient, but need to be associated here. 
        public Address PlantAddress { get; set; }
        public DateTime? IssuedUtc { get; set; }
        public DateTime? ExpiryUtc { get; set; }
        
        public Guid? PersonId { get; set; }
        [ForeignKey("PersonId")]
        public Person Person { get; set; }
    }
}
