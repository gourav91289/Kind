using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OmniPot.Data.Models
{
    public class TestDefinition : EntityBase
    {
        public Guid TestDefinitionId { get; set; }
        [StringLength(50)]
        //NOTE: Will be THCA, BetaMyrcene, etc..
        public string DisplayName { get; set; }

        public bool? IsPassFail { get; set; }
        public bool RequiresDecimal { get; set; }
        public bool RequiresNote { get; set; }
        public int DisplayOrder { get; set; }
        
    }
}
