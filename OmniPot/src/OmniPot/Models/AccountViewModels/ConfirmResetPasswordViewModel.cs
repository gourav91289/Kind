using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace OmniPot.Models.AccountViewModels
{
    public class ConfirmResetPasswordViewModel
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public string NewPassword { get; set; }
    }
}
