using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OmniPot.Data.Models
{
    public class License : EntityBase
    {
        public Guid LicenseId { get; set; }
        public string LicenseNumber { get; set; }
        public int ExpiryMonth { get; set; }
        public int ExpiryYear { get; set; }
    }
}
