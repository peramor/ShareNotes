using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShareNotesV3.Models
{
    [Table("Faculties")]
    public partial class Faculty
    {
        public Faculty()
        {
            Courses = new HashSet<Course>();
            Students = new HashSet<Student>();
        }

        public int FacultyId { get; set; }
        public int CampusId { get; set; }
        public string Name { get; set; }
        public int? DegreeId { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
        public virtual ICollection<Student> Students { get; set; }
        public virtual Campus Campus { get; set; }
        public virtual Degree Degree { get; set; }
    }
}
