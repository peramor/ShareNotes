using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShareNotesV3.Models
{
    [Table("Notes")]
    public partial class Note
    {
        public Note()
        {
            Markers = new HashSet<Marker>();
            Views = new HashSet<View>();
        }

        public int NoteId { get; set; }
        public int LectureId { get; set; }
        public int? StudentId { get; set; }
        public string Document { get; set; }

        public virtual ICollection<Marker> Markers { get; set; }
        public virtual ICollection<View> Views { get; set; }
        public virtual Lecture Lecture { get; set; }
        public virtual Student Student { get; set; }
    }
}
