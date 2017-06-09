using OmniPot.Data;
using OmniPot.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
