using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OmniPot.Data.Models
{
    public class Vendor : EntityBase
    {
        public Guid VendorId { get; set; }
        public Guid TenantId { get; set; }
        [StringLength(255)]
        public string DisplayName { get; set; }
        [StringLength(100)]
        public string BusinessDbaName { get; set; }
        [StringLength(50)]
        public string ContactFirstName { get; set; }
        [StringLength(50)]
        public string ContactMiddleName { get; set; }
        [StringLength(50)]
        public string ContactLastName { get; set; }
        [EmailAddress]
        public string ContactEmailAddress { get; set; }
        [Phone]
        public string ContactPhone { get; set; }
        [Phone]
        public string ContactCellPhone { get; set; }
        [StringLength(1000)]
        public string PublicNotes { get; set; }
        [StringLength(1000)]
        public string PrivateNotes { get; set; }
        public ClassificationType VendorType { get; set; }

        public bool IsTaxExempt { get; set; }

        [StringLength(50)]
        public string LicenseNumber { get; set; }
        public DateTime LicenseExpiry { get; set; }

        [StringLength(20)]
        public string Fein { get; set; }
        [StringLength(20)]
        public string SalesTaxId { get; set; }
        [StringLength(20)]
        public string InsuranceProvider { get; set; }
        [StringLength(20)]
        public string InsuranceDocumentNumber { get; set; }
        [StringLength(8)]
        [RegularExpression("^[0-9]{4,8}$")]
        public string DeliveryPin { get; set; }

        public Guid? BillingAddressId { get; set; }
        [ForeignKey("BillingAddressId")]        
        public Address BillingAddress { get; set; }
        //Don't always need a shipping address.
        public Guid? ShippingAddressId { get; set; }
        [ForeignKey("ShippingAddressId")]
        public Address ShippingAddress { get; set; }

        //TODO: Price groups

        public IList<UploadDocument> Documents { get; set; } = new List<UploadDocument>();
        public IList<Contact> Contacts { get; set; } = new List<Contact>();

    }
}
