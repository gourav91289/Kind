using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace OmniPot.Data.Identity
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {

        public int AuthyCountyCode { get; set; } = 1;
        public string AuthyUserId { get; set; }

        public string AuthyCarrier { get; set; }

        public bool AuthyIsPorted { get; set; }

        public bool AuthyIsCellPhone { get; set; }

    }
}
