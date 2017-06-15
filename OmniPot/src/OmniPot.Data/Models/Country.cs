using System;
using System.ComponentModel.DataAnnotations;

namespace OmniPot.Data.Models
{
    public class Country : EntityBase
    {
        public Guid CountryId { get; set; }
        
        [Display(Name = "Name")]
        [StringLength(255)]
        public string DisplayName { get; set; }

        [StringLength(5)]
        public string Abbreviation { get; set; }

    }
}
