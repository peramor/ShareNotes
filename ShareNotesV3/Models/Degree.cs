using System;
using System.Collections.Generic;

namespace ShareNotesV3.Models
{
    public partial class Degree
    {
        public Degree()
        {
            Faculties = new HashSet<Faculty>();
        }

        public int DegreeId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Faculty> Faculties { get; set; }
    }
}
