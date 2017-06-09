using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OmniPot.Data.Models
{
    public enum Ethnicity
    {
        [Display(Name = "Hispanic or Latino")]
        HispanicOrLatino = 2, 
        [Display(Name = "Not Hispanic or Latino")]
        NotHispanicOrLatino = 4
    }
}
