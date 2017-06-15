using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OmniPot.Data.Models
{
    public class City : EntityBase
    {
        public Guid CityId { get; set; }

        [StringLength(50)]
        public string CityName { get; set; }

        public Guid StateId { get; set; }

        [ForeignKey("StateId")]
        public State States { get; set; }

    }
}
