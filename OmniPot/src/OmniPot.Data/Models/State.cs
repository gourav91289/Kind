using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OmniPot.Data.Models
{
    public class State : EntityBase
    {
        public Guid StateId { get; set; }

        [StringLength(30)]
        public string StateName { get; set; }

        public Guid CountryId { get; set; }

        [ForeignKey("CountryId")]
        public Country Country { get; set; }
    }
}
