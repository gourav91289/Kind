using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OmniPot.Models.StateRegistrationViewModels
{
    public class AddIndividualViewModel : CommonViewModel
    {
        public string LicenseNumber { get; set; }
        public int LicenseExpiryMonth { get; set; }
        public int LicenseExpiryYear { get; set; }
    }
}
