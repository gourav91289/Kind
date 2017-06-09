using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OmniPot.Data.Models
{
    public class RfidTag
    {
        /// <summary>
        /// Not the tag data but the database id
        /// </summary>
        [Key]
        public Guid RfidTagId {get;set;}

        //TODO: This could be represented better by a string depending on data
        //TODO: Break out the tag data into it's respective parts here too if there's an easy way to parse out just the tag id
        public byte[] TagData { get; set; }
        public DateTime? DestroyedUtc { get; set; }

        public Guid? ReplacedById { get; set; }
        public DateTime? ReplacedUtc { get; set; }
    }
}
