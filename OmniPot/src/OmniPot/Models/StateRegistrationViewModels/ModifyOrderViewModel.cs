using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OmniPot.Models.StateRegistrationViewModels
{
    public class ModifyOrderViewModel
    {
        public Guid OrderId { get; set; }
        public Guid OrderItemId { get; set; }
        public int Quantity { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
