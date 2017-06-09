using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OmniPot.Data.Models
{
    public class TestResult
    {
        public Guid TestResultId { get; set; }
        public Guid TestDefinitionId { get; set; }
        [ForeignKey("TestDefinitionId")]
        public TestDefinition TestDefinition { get; set; }

        [StringLength(25)]
        [Required]
        public string StrainName { get; set; }

        public bool? HasPassed { get; set; }

        //TODO: precision
        public decimal Result { get; set; }
        [StringLength(100)]
        public string ResultNotes { get; set; }

        public DateTime TestDate { get; set; }
    }
}
