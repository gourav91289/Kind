using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OmniPot.Data.Models
{
    public class ServiceRecord : EntityBase
    {
        public Guid ServiceRecordId { get; set; }
        public Guid VehicleId { get; set; }
        //No tenant, get that from vehicle. 
        [ForeignKey("VehicleId")]
        public Vehicle Vehicle { get; set; }
        public VehicleServiceType ServiceType { get; set; }
        public DateTime ServiceDate { get; set; }
        public Guid? VendorId { get; set; }
        public DateTime NextServiceDate { get; set; }
        public int CurrentMilage { get; set; }
        [StringLength(500)]
        public string Notes { get; set; }
        public ICollection<UploadDocument> Documents { get; set; } = new List<UploadDocument>();
    }
}
