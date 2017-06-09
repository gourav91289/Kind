using OmniPot.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace OmniPot.Data
{
    public abstract class EntityBase : ITrackableEntity
    {
        [ScaffoldColumn(false)]
        public Guid CreatedByUserId { get; set; }
        [ScaffoldColumn(false)]
        public DateTime CreatedUtc { get; set; }
        [ScaffoldColumn(false)]
        public Guid ModifiedByUserId { get; set; }
        [HiddenInput(DisplayValue = false)]
        [Timestamp]
        public byte[] RowVersion { get; set; }
        [ScaffoldColumn(false)]
        public TrackableEntityState State { get; set; }
        [ScaffoldColumn(false)]
        public DateTime ModifiedUtc { get; set; }
    }
}
