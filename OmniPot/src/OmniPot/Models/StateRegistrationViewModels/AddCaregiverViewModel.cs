using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OmniPot.Models.StateRegistrationViewModels
{
    public class AddCaregiverViewModel : CommonViewModel
    {
        public string LicenseNumber { get; set; }
        public int LicenseExpiryMonth { get; set; }
        public int LicenseExpiryYear { get; set; }
        public string GrowLocationAddress { get; set; }
        public string GrowLocationPostalCode { get; set; }
        public bool IsAlsoPatient { get; set; }
        public string PatientLicenseNumber { get; set; }
        public int PatientLicenseExpiryMonth { get; set; }
        public int PatientLicenseExpiryYear { get; set; }
    }
}
