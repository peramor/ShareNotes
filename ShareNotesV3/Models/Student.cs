using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShareNotesV3.Models
{
    [Table("Students")]
    public partial class Student
    {
        public Student()
        {
            Markers = new HashSet<Marker>();
            Notes = new HashSet<Note>();
            Views = new HashSet<View>();
        }

        public int StudentId { get; set; }
        [Display(Name = "Факультет")]
        public int? FacultyId { get; set; }
        [Display(Name = "Имя")]
        public string FullName { get; set; }
        [Display(Name = "Email")]
        public string Mail { get; set; }
        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        public string Pwd { get; set; }

        public virtual ICollection<Marker> Markers { get; set; }
        public virtual ICollection<Note> Notes { get; set; }
        public virtual ICollection<View> Views { get; set; }
        [Display(Name = "Факультет")]
        public virtual Faculty Faculty { get; set; }
    }
}
