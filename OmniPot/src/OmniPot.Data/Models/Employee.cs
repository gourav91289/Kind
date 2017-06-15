using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OmniPot.Data.Models
{
    public class Employee : EntityBase
    {
        public Guid EmployeeId { get; set; }
        public Guid TenantId { get; set; }
        public Guid? SupervisorId { get; set; }
        [ForeignKey("SupervisorId")]
        public Employee Supervisor { get; set; }
        [Display(Name = "First Name")]
        [StringLength(100)]
        public string FirstName { get; set; }
        [Display(Name = "Middle Name")]
        [StringLength(100)]
        public string MiddleName { get; set; }
        [Display(Name = "Last Name")]
        [StringLength(100)]
        public string LastName { get; set; }
        public EmploymentStatus Status { get; set; }
        public CompensationType CompensationType { get; set; }
        //TODO: Commission level
        [Phone]
        public string MobilePhone { get; set; }
        [Phone]
        public string OtherPhone { get; set; }
        [StringLength(100)]
        public string Title { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime DateOfHire { get; set; }
        public DateTime DateOfTermination { get; set; }
        [StringLength(50)]
        public string LicenseNumber { get; set; }
        public DateTime LicenseExpiry { get; set; }
        public Guid? AddressId { get; set; }
        [ForeignKey("AddressId")]
        public Address Address { get; set; }
        public string UserId { get; set; }
        public bool IsDriver { get; set; }
        public Guid? VehicleId { get; set; }
        [ForeignKey("VehicleId")]
        public Vehicle Vehicle { get; set; }

        public ICollection<Location> AllowedLocations { get; set; } = new List<Location>();

        public ICollection<UploadDocument> Documents { get; set; } = new List<UploadDocument>();

    }
}
