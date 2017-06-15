using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OmniPot.Data.Models
{
    public class ItemType : EntityBase
    { 
        public Guid ItemTypeId { get; set; }
        public Guid? TenantId { get; set; }
        [StringLength(50)]
        public string DisplayName { get; set; }
        public Guid? ParentItemTypeId { get; set; }
        [ForeignKey("ParentItemTypeId")]
        public ItemType ParentType { get; set; }
        public ICollection<ItemType> Children { get; set; } = new List<ItemType>();
        public ICollection<LocationType> LocationTypes { get; set; } = new List<LocationType>();
    }
}
