using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OmniPot.Data
{
    [Flags]
    public enum TrackableEntityState
    {
        None = 0,
        [Display(Name = "Active")]
        IsActive = 1, 
        [Display(Name = "Deleted")]
        IsDeleted = 2, 
        [Display(Name = "Locked")]
        IsLocked = 4
        //NOTE: Do not add states without team discussion. These are intended to be shared amongst all entities.
    }
}
