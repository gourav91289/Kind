using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OmniPot.Models.StateRegistrationViewModels
{
    public class ProvisionTagsViewModel
    {
        public Guid PersonId { get; set; }
        /// <summary>
        /// Address must already exist for the location of these plants and will relate to an individuals address or the business location
        /// </summary>
        
        public Guid AddressId { get; set; }
        public int Quantity { get; set; }
    }
}
