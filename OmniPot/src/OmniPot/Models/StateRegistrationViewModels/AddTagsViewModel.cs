using OmniPot.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OmniPot.Models.StateRegistrationViewModels
{

    public class AddTagsViewModel
    {
        public Guid PersonId { get; set; }
        public int Quantity { get; set; }
        public Guid AddressId { get; set; }
        public TagType TagType { get; set; }
        public string LicenseNumber { get; set; }
    }
}
