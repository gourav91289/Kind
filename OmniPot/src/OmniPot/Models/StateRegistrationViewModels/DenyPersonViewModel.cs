using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OmniPot.Models.StateRegistrationViewModels
{
    public class DenyPersonViewModel
    {
        public Guid PersonId { get; set; }
        public List<Guid> People { get; set; }
        //NOTE: This wasn't in the screen but it makes sense to have. 
        public string Reason { get; set; }
    }
}
