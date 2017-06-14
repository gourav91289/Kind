using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OmniPot.Data.Models
{
    public class UserInvitation : EntityBase
    {
        public Guid UserInvitationId { get; set; }

        [StringLength(100)]
        public string FirstName { get; set; }

        [StringLength(100)]
        public string MiddleName { get; set; }

        [StringLength(100)]
        public string LastName { get; set; }

        public int InvitionText { get; set; }

        public bool IsInvitationAccepted { get; set; }

        [ForeignKey("TenantId")]
        public Tenant Tenant { get; set; }

    }
}
