using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OmniPot.Data.Models
{
    public class NoteEntry : EntityBase
    {
        public Guid NoteEntryId { get; set; }
        public Guid NoteId { get; set; }
        public string Text { get; set; }
        /// <summary>
        /// Easy way to determine in grids who made the entry (As set on creation) so we don't need to hit the user table over it.
        /// </summary>
        public string MadeByEmailAddress { get; set; }
    }
}
