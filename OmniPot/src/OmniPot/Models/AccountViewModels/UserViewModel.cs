using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OmniPot.Models.AccountViewModels
{
    public class UserViewModel
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public DateTime LastLogin { get; set; }
        public string PhoneNumber { get; set; }
        public bool EmailConfirmed { get; set; }
    }
}
