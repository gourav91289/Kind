using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OmniPot.Data.Models
{
    public class Contact : EntityBase
    {
        public Guid ContactId { get; set; }
        [StringLength(50)]
        public string DisplayName { get; set; }
        [StringLength(50)]
        public string Title { get; set; }
        [EmailAddress]
        public string EmailAddress { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
    }
}
