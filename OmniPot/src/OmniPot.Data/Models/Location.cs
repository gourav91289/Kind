using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OmniPot.Data.Models
{
    public class Location : EntityBase
    {
        public Guid LocationId { get; set; }
        [StringLength(50)]
        public string DisplayName { get; set; }
        [StringLength(50)]
        public string RouteName { get; set; }

        public Guid TenantId { get; set; }

        [ForeignKey("TenantId")]
        public Tenant Tenant { get; set; }

        //NOTE: Only ParentLocations need an address
        public Guid? AddressId { get; set; }

        [ForeignKey("AddressId")]
        public Address Address { get; set; }

        public Guid? ParentLocationId { get; set; }

        [ForeignKey("ParentLocationId")]
        public Location ParentLocation { get; set; }

        /// <summary>
        /// Makes the determination as to whether this location can have salable inventory pulled from it. I.E., a 
        /// sales counter location, not necessarily a backroom location.
        /// </summary>
        public bool IsSalable { get; set; }

        public Guid LocationTypeId { get; set; }
        public LocationType LocationType { get; set; }

        public ClassificationType ClassificationType { get; set; }
        public ICollection<Location> Children { get; set; } = new List<Location>();

        public ICollection<Batch> Batches { get; set; } = new List<Batch>();
        public ICollection<Lot> Lots { get; set; } = new List<Lot>();

        public ICollection<UserLocation> UserLocations { get; set; } = new List<UserLocation>();
     }
}
