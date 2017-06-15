using System;
using OmniPot.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

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

        public bool IsActive { get; set; }

        public Guid TenantId { get; set; }

        [ForeignKey("TenantId")]
        public Tenant Tenant { get; set; }

    }
}
