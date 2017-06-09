using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OmniPot.Data.Models
{
    public class Person : EntityBase
    {
        public Guid PersonId { get; set; }

        public Guid AddressId { get; set; }
        [ForeignKey("AddressId")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Address is required")]
        public Address Address { get; set; }
        public Guid GrowAddressId { get; set; }
        [ForeignKey("GrowAddressId")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Grow Address is required")]
        public Address GrowAddress { get; set; }
        public bool IsGrowAddressCooperative { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }
        public string CompanyName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        [EmailAddress]
        [Required(ErrorMessage = "E-mail address is required")]
        public string EmailAddress { get; set; }
        public Ethnicity Ethnicity { get; set; }
        public Race Race { get; set; }
        public MaritalStatus MaritalStatus { get; set; }
        public bool IsVeteran { get; set; }
        public bool IsCitizen { get; set; } 
        public EducationStatus EducationStatus { get; set; }
        public bool IsCurrentlyEnrolled { get; set; }
        public EmploymentStatus EmploymentStatus { get; set; }
        public string CurrentOccupation { get; set; }

        //This is a temporary stop gap for phase one before we can actually provision tags. 
        public int RequestedTagCount { get; set; }

        public ApplicationStatus ApplicationStatus { get; set; }
        public DateTime? ApprovedUtc { get; set; }
        public DateTime? DeniedUtc { get; set; }
        public string DeniedReason { get; set; }
        public bool IsAlsoPatient { get; set; }
        public PersonType PersonType { get; set; }

        public bool HasDohDiscount { get; set; }
        public virtual List<Phone> PhoneNumbers { get; set; }

        public virtual List<License> Licenses { get; set; }
        public Guid? PatientLicenseId { get; set; }
        [ForeignKey("PatientLicenseId")]
        public License PatientLicense { get; set; }

        public virtual List<PaymentTransaction> Payments { get; set; }

        //public virtual List<PlantTag> PlantTags { get; set; }
    }
}
