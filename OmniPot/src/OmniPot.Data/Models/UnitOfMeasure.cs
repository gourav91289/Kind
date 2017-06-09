using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OmniPot.Data.Models
{
    public class UnitOfMeasure : EntityBase
    {
        public Guid UnitOfMeasureId { get; set; }

        [Required]
        [StringLength(50)]
        public string DisplayName { get; set; }

        [StringLength(10)]
        [Required]
        public string Abbreviation { get; set; }
    }
}
