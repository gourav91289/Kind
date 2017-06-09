using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OmniPot.Data.Models
{
    public class Phone : EntityBase
    {
        public Guid PhoneId { get; set; }
        public PhoneType PhoneType { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
    }
}
