﻿using System;
using System.Linq;
using OmniPot.Data.Identity;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OmniPot.Data.Models
{
    public class UserLocation : EntityBase
    {
        public Guid UserLocationId { get; set; }

        //public Guid UserId { get; set; }

        //[ForeignKey("UserId")]
        //public ApplicationUser User { get; set; }

        public Guid LocationId { get; set; }

        [ForeignKey("LocationId")]
        public Location Location { get; set; }

        public Guid TenantId { get; set; }

        [ForeignKey("TenantId")]
        public Tenant Tenant { get; set; }

    }
}
