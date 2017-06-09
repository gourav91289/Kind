using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OmniPot.Data.Models
{
    public class UploadDocument : EntityBase
    {
        public Guid UploadDocumentId { get; set; }
        public Guid TenantId { get; set; }
        [StringLength(255)]
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public DateTime ExpirationUtc { get; set; }
        public bool RemoveUponExpiry { get; set; }
        [StringLength(100)]
        public string ContentType { get; set; }
        public byte[] FileData { get; set; }
    }
}
