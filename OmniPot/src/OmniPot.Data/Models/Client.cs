using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OmniPot.Data.Models
{
    public class Client : EntityBase
    {
        public Guid ClientId { get; set; }
        public Guid TenantId { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "First name is required")]
        [StringLength(50)]
        public string FirstName { get; set; }
        [StringLength(50)]
        public string MiddleName { get; set; }
        [StringLength(50)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Middle name is required")]
        public string LastName { get; set; }
        [StringLength(50)]
        public string NickName { get; set; }

        public Gender Gender { get; set; }

        public DateTime DateOfBirth { get; set; }
        public bool IsTaxExempt { get; set; }

        public bool IsTempStatus { get; set; }
        [Phone]
        public string CellPhone { get; set; }
        [Phone]
        public string BusinessPhone { get; set; }
        [EmailAddress]
        public string EmailAddress { get; set; }
        //TODO: SSN Makes me nervous
        
        public ClassificationType ClientType { get; set; }

        [StringLength(1000)]
        public string PublicNotes { get; set; }
        [StringLength(1000)]
        public string PrivateNotes { get; set; }
        public string PrivateNotesExtended { get; set; }

        //TODO: Determine pricing groups

        public Guid? MailingAddressId { get; set; }
        [ForeignKey("MailingAddressId")]
        public Address MailingAddress { get; set; }

        public Guid? DeliveryAddressId { get; set; }
        [ForeignKey("DeliveryAddressId")]
        public Address DeliveryAddress { get; set; }

        public Guid DefaultSaleLocationId { get; set; }
        [ForeignKey("DefaultSaleLocationId")]
        //TODO: Hrm, do we need to be able to navigate to this?
        public Location DefaultSaleLocation { get; set; }

        public IList<UploadDocument> Documents { get; set; } = new List<UploadDocument>();
        public IList<Contact> Contacts { get; set; } = new List<Contact>();

    }
}
