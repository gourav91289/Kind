using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OmniPot.Data.Models
{
    public class Vehicle : EntityBase
    {
        public Guid VehicleId { get; set; }
        public Guid TenantId { get; set; }
        [StringLength(25)]
        public string Vin { get; set; }
        [StringLength(30)]
        public string Make { get; set; }
        [StringLength(50)]
        public string Model { get; set; }
        [StringLength(4)]
        public string Year { get; set; }
        [StringLength(25)]
        public string Color { get; set; }
        [StringLength(20)]
        public string PlateNumber { get; set; }
        [StringLength(255)]
        public string GpsId { get; set; }
        [StringLength(255)]
        public string MiscData { get; set; }
        //TODO: Next service from service records.
        public ICollection<ServiceRecord> ServiceRecords { get; set; } = new List<ServiceRecord>();
        public ICollection<UploadDocument> Documents { get; set; } = new List<UploadDocument>();
    }
}
