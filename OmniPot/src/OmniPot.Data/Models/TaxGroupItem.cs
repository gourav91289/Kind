using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OmniPot.Data.Models
{
    public class TaxGroupItem
    {
        public Guid TaxGroupId { get; set; }
        public TaxGroup TaxGroup { get; set; }
        public Guid TaxItemId { get; set; }
        public TaxItem TaxItem { get; set; }
    }
}
