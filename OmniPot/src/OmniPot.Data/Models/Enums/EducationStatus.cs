using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OmniPot.Data.Models
{
    [Flags]
    public enum EducationStatus
    {
        [Display(Name = "Some High School Completed")]
        SomeHighSchool = 2, 
        [Display(Name = "High School Diploma / GED")]
        HighSchool = 4, 
        [Display(Name = "Technical School")]
        TechnicalScool = 8, 
        [Display(Name = "Community College / 2-yr Degree")]
        CommunityCollege = 16, 
        [Display(Name = "University / 4-yr Degree")]
        University = 32, 
        [Display(Name = "Master Program or Above")]
        GraduateDegree = 64
    }
}
