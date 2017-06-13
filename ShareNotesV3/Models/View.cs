using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShareNotesV3.Models
{
    [Table("Views")]
    public partial class View
    {
        public int NoteId { get; set; }
        public int StudentId { get; set; }
        public int? Rate { get; set; }
        public bool? Favorite { get; set; }

        public virtual Note Note { get; set; }
        public virtual Student Student { get; set; }
    }
}
