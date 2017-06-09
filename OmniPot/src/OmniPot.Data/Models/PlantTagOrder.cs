using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OmniPot.Data.Models
{
    public class PlantTagOrder : EntityBase
    {
        public Guid PlantTagOrderId { get; set; }
        public Guid PersonId { get; set; }
        public DateTime OrderDateUtc { get; set; }
        public int TagQuantity { get; set; }
        public decimal Amount { get; set; }
        //Token returned from the payment service 
        public string Token { get; set; }

        public decimal Tax { get; set; }
        public virtual List<PlantTagOrderItem> OrderItems { get; set; } = new List<PlantTagOrderItem>();

    }
}
