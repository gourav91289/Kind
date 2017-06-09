using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OmniPot.Data.Models
{
    public class StateOrProvince : EntityBase
    {
        public Guid StateOrProvinceId { get; set; }
        [Display(Name ="Name")]
        [StringLength(100)]
        public string DisplayName { get; set; }
        [StringLength(5)]
        public string Abbreviation { get; set; }

        [Display(Name = "Country")]
        public Guid CountryId { get; set; }
        [ForeignKey("CountryId")]
        public Country Country { get; set; }
    }
}
