using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OmniPot.Data.Models
{
    public class Note : EntityBase
    {
        public Guid NoteId { get; set; }
        [Required]
        public Guid ObjectId { get; set; }
        public NoteType NoteType { get; set; }
        public NoteStatus NoteStatus { get; set; }
        public DateTime DeadlineUtc { get; set; }
        [Required]
        [StringLength(255)]
        public string Summary { get; set; }
        public virtual List<NoteEntry> Entries { get; set; }
    }
}
