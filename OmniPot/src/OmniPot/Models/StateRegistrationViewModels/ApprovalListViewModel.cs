using OmniPot.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OmniPot.Models.StateRegistrationViewModels
{
    public class ApprovalListViewModel
    {
        public Guid PersonId { get; set; }
        public string FullName { get; set; }
        public string CompanyName { get; set; }
        public string LicenseNumber { get; set; }
        public string EmailAddress { get; set; }
        public PersonType PersonType {get; set;}
        public DateTime ExpiryUtc { get; set; }
    }
}
