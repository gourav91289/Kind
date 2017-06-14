using System;
using OmniPot.Data.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OmniPot.Data.Models
{
    public class UserDetail : EntityBase
    {
        public Guid CountryId { get; set; }

        //[ForeignKey("Id")]
        //public ApplicationUser UserId { get; set; }

        [StringLength(100)]
        public string FirstName { get; set; }

        [StringLength(100)]
        public string MiddleName { get; set; }

        [StringLength(100)]
        public string LastName { get; set; }

        [ForeignKey("UserInvitationId")]
        public UserInvitation UserInvitations { get; set; }

        [StringLength(18)]
        public int PhoneNo { get; set; }

        [StringLength(18)]
        public int MobileNo { get; set; }

        [StringLength(500)]
        public string Address1 { get; set; }

        [StringLength(500)]
        public string Address2 { get; set; }

        [StringLength(500)]
        public string Address3 { get; set; }

        [StringLength(10)]
        public string PostalCode { get; set; }

        [ForeignKey("CityId")]
        public City City { get; set; }

        [ForeignKey("StateId")]
        public State States { get; set; }

        [ForeignKey("CountryId")]
        public Country Country { get; set; }

    }
}
