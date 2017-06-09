using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OmniPot.Data.Models
{
    public class TestRule
    {
        public Guid TestRuleId { get; set; }
        public Guid LocationId { get; set; }
        [ForeignKey("LocationId")]
        public Location Location { get; set; }
        public Guid TestDefinitionId { get; set; }
        [ForeignKey("TestDefinitionId")]
        public TestDefinition TestDefinition { get; set; }
        public bool IsRequired { get; set; }
    }
}
