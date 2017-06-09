using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OmniPot.Models.StateRegistrationViewModels
{
    public class ValidateLicenseViewModel
    {
        public string LicenseNumber { get; set; }
        //TODO: Will we need additional stuff like name? DOB?
        public int LicenseExpiryMonth { get; set; }
        public int LicenseExpiryYear { get; set; }
        public string CaretakerLicense { get; set; }
        public int CaretakerLicenseExpiryMonth { get; set; }
        public int CaretakerLicenseExpiryYear { get; set; }


    }
}
