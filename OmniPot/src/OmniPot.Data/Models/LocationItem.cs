using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OmniPot.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace OmniPot.Data.Models
{
    public class LocationItem : EntityBase
    {
        public Guid LocationItemId { get; set; }

        public Guid LocationId { get; set; }

        [ForeignKey("LocationId")]
        public Location Location { get; set; }
        public Guid ItemTypeId { get; set; }

        [ForeignKey("ItemTypeId")]
        public ItemType ItemType { get; set; }

        public Guid? ItemTypeId { get; set; }

        [ForeignKey("ItemTypeId")]
        public ItemType ItemType { get; set; }

        public ICollection<ItemType> ItemTypes { get; set; } = new List<ItemType>();

    }
}
