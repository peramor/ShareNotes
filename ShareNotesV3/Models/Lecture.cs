using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShareNotesV3.Models
{
    [Table("Lectures")]
    public partial class Lecture
    {
        public Lecture()
        {
            Notes = new HashSet<Note>();
        }

        public int LectureId { get; set; }
        public int CourseId { get; set; }
        public DateTime Date { get; set; }
        public string Topic { get; set; }
        public string Abstract { get; set; }

        public virtual ICollection<Note> Notes { get; set; }
        public virtual Course Course { get; set; }
    }
}
