using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OmniPot.Models.StateRegistrationViewModels
{
    public class RemoveOrderItemViewModel
    {
        [Required]
        public Guid OrderId {get;set;}
        [Required]
        public Guid OrderItemId { get; set; }
    }
}
