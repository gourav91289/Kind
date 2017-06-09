using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OmniPot.Data.Models
{
    public enum EmploymentStatus
    {
        Unemployeed = 2,
        [Display(Name = "Part-time")]
        PartTime = 4,
        [Display(Name = "Full-time")]
        FullTime = 8,
    }
}
