using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OmniPot.Models.StateRegistrationViewModels
{
    public class CompletePaymentViewModel
    {
        public string PaymentToken { get; set; }
        public Guid OrderId { get; set; }
        public bool IsSuccessful { get; set; }
    }
}
