using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations; 

namespace OmniPot.Models.StateRegistrationViewModels
{
    public class CommonViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StreetAddressPrimary { get; set; }
        public string StreetAddressSecondary { get; set; }
        public string City { get; set; }
        public Guid StateId { get; set; }
        [StringLength(5)]
        public string PostalCode { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }

        public string GrowAddressPrimary { get; set; }
        public string GrowAddressSecondary { get; set; }
        public string GrowAddressCity { get; set; }
        [StringLength(5)]
        public string GrowAddressPostalCode { get; set; }
        public Guid GrowAddressStateId { get; set; }
        public bool GrowAddressIsCooperative { get; set; }
        public string EmailAddress { get; set; }
        public bool HasDohDiscount { get; set; }
    }
}
