using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShareNotesV3.Models
{
    [Table("Courses")]
    public partial class Course
    {
        public Course()
        {
            Lectures = new HashSet<Lecture>();
        }

        public int CourseId { get; set; }
        public int? FacultyId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Lecture> Lectures { get; set; }
        public virtual Faculty Faculty { get; set; }
    }
}
