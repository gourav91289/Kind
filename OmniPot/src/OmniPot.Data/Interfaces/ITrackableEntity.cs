using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OmniPot.Data.Interfaces
{
    public interface ITrackableEntity
    {
        [Display(Name = "Last Updated (UTC)")]
        DateTime ModifiedUtc { get; set; }
        [Display(Name = "Created On (UTC)")]
        DateTime CreatedUtc { get; set; }
        Guid ModifiedByUserId { get; set; }
        Guid CreatedByUserId { get; set; }
        TrackableEntityState State { get; set; }
        [Timestamp]
        byte[] RowVersion { get; set; }
    }
}
