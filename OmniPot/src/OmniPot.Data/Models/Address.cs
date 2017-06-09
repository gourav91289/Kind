using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OmniPot.Data.Models
{
    public class Address : EntityBase
    {
        public Guid AddressId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Addressee is required")]
        [StringLength(50)]
        public string Addressee { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Address is required")]
        [StringLength(50)]
        public string DeliveryLine1 { get; set; }
        [StringLength(50)]
        public string DeliveryLine2 { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "City is required")]
        [StringLength(64)]
        public string CityName { get; set; }

        public Guid? StateOrProvinceId { get; set; }
        [ForeignKey("StateOrProvinceId")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "State is required")]
        public StateOrProvince StateOrProvince { get; set; }

        [StringLength(10)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Postal code is required")]
        public string PostalCode { get; set; }
    }
}
