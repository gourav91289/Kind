using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OmniPot.Models.StateRegistrationViewModels
{
    public class CheckoutViewModel
    {
        public Guid PersonId { get; set; }
        public Guid OrderId { get; set; }
    }
}
