using OmniPot.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OmniPot.Data.Models
{
    public class Batch : EntityBase
    {
        public Guid BatchId { get; set; }
        public Guid LocationId { get; set; }
        [ForeignKey("LocationId")]
        public Location Location { get; set; }
        public Guid InventoryItemId { get; set; }
        [ForeignKey("InventoryItemId")]
        public InventoryItem InventoryItem { get; set; }
        public DateTime ManufactureDateUtc { get; set; }
        public DateTime ExpiryDateUtc { get; set; }
        //NOTE: Is also DisplayName
        public string BarCode { get; set; }
//        [NotMapped] not implemented in EF7 yet
        public decimal Weight
        {
            get { return Lots.Sum(e => e.Weight); }
        }

        public int Quantity
        {
            get { return Lots.Sum(e => e.Quantity); }
        }
        //NOTE: There must be at least one Lot to a Batch
        [EnsureMinimumElements(1, ErrorMessage = "At least one Lot needs to be associated with the batch")]
        public IList<Lot> Lots { get; set; } = new List<Lot>();
        //NOTE: Batch will also have IList<Plants> when cultivation is put together.
    }
}
