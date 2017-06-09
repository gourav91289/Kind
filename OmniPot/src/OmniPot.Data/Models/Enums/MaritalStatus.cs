using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OmniPot.Data.Models
{
    [Flags]
    public enum MaritalStatus
    {
        Single = 2, 
        Married = 4,
        Divorced = 8, 
        Separated = 16, 
        Widowed = 32, 
        [Display(Name = "Unmarried Partnership")]
        UnmarriedPartnership = 64

    }
}
