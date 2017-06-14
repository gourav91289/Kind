using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OmniPot.Data.Models
{
    public class Tenant : EntityBase
    {
        public Guid TenantId { get; set; }

        [StringLength(50)]
        public string DisplayName { get; set; }

        [StringLength(25)]
        public string RouteName { get; set; }

        [StringLength(100)]
        public string Theme { get; set; }
        public string CssOverrides { get; set; }

        public Guid AddressId { get; set; }

        [ForeignKey("AddressId")]
        public Address Address { get; set; }

        public bool UseLots { get; set; }

        public ICollection<Location> Locations { get; set; } = new List<Location>();

        public ICollection<Employee> Employees { get; set; } = new List<Employee>();

        public ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();

        public ICollection<Vendor> Vendors { get; set; } = new List<Vendor>();

        public ICollection<Client> Clients { get; set; } = new List<Client>();

        public ICollection<InventoryItem> InventoryItems { get; set; } = new List<InventoryItem>();
        public ICollection<UserLocation> UserLocations { get; set; } = new List<UserLocation>();

        // public ICollection<TaxItem> TaxItems { get; set; } = new List<TaxItem>();
        //TODO: Determine if we want inventory relations coming from tenant
    }
}
