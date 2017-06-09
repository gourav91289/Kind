using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OmniPot.Data.Models
{
    [Flags]
    public enum Race
    {
        [Display(Name = "Caucasian / White")]
        CaucasianOrWhite = 2, 
        [Display(Name = "African American / Black")]
        AfricanAmericanOrBlack = 4, 
        Asian = 8, 
        [Display(Name = "American Indian or Alaskan Native")]
        AmericanIndianOrAlaskanNative = 16, 
        [Display(Name = "Hawaiian Native or Pacific Islander")]
        NativeHawaiianOrPacificIslander = 32, 
        Other = 0
    }
}
