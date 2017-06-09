using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OmniPot.Data.Models
{
    public class Lot : EntityBase
    {
        public Guid LotId { get; set; }
        public Guid BatchId { get; set; }
        [ForeignKey("BatchId")]
        public Batch Batch { get; set; }
        public Guid CurrentLocationId { get; set; }
        [ForeignKey("CurrentLocationId")]
        public Location CurrentLocation { get; set; }
        public Guid UnitOfMeasureId { get; set; }
        [ForeignKey("UnitOfMeasureId")]
        public UnitOfMeasure UnitOfMeasure { get; set; }        
        public decimal Weight { get; set; }
        //NOTE: Batch Quantity is a sum of all lot qty's
        public int Quantity { get; set; }
        [StringLength(1000)]
        public string Notes { get; set; }

        //TODO: type? other?Reason?? OldQty?
        public string BarCode { get; set; }

        public IList<TestResult> TestResults { get; set; } = new List<TestResult>();

    }
}
