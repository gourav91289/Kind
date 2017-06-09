using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OmniPot.Data.Models
{
    public class TaxGroup : EntityBase
    {
        public Guid TaxGroupId { get; set; }
        public Guid LocationId { get; set; }
        [ForeignKey("LocationId")]
        public Location Location { get; set; }
        [StringLength(50)]
        public string DisplayName { get; set; }
        [StringLength(20)]
        public string DisplayColor { get; set; }
        public bool IsAppliedBeforeDiscount { get; set; }
        //rate is not set for a tax group but calculated based on tax items. Looking into the best way to apply this
        public IList<TaxGroupItem> TaxGroupItems { get; set; } = new List<TaxGroupItem>();
    }
}
