using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OmniPot.Data.Models
{
    public class PlantTagOrderItem : EntityBase
    {
        public Guid PlantTagOrderItemId { get; set; }
        public Guid PlantTagOrderId { get; set; }
        public Guid AddressId { get; set; }
        public TagType TagType { get; set; }
        public string Sku { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
