using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace OmniPot.Models.StateRegistrationViewModels
{
    public class AddBusinessViewModel : CommonViewModel
    {
        [Required]
        public string BusinessName { get; set; }
        [Required]
        [MaxLength(14, ErrorMessage = "License is invalid.")]
        public string LicenseNumber { get; set; }
        [Required]
        [Range(1, 12, ErrorMessage = "Expiry month is invalid.")]
        public int LicenseExpiryMonth { get; set; }
        [MaxLength(4, ErrorMessage = "Expiry year is invalid.")]
        //TODO: Better testing
        public int LicenseExpiryYear { get; set; }
        public string GrowLocationAddress { get; set; }
        [StringLength(5, ErrorMessage = "Zip code is invalid.", MinimumLength = 5)]
        public string GrowLocationPostalCode { get; set; }
        //TODO: Needs to support multiple grow locations
    }
}
