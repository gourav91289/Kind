using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OmniPot.Models.StateRegistrationViewModels
{
    public class ApprovePersonViewModel
    {
        public Guid PersonId { get; set; }
        public List<Guid> People { get; set; }
    }
}
