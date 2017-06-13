using System;
using System.Collections.Generic;

namespace ShareNotesV3.Models
{
    public partial class Campus
    {
        public Campus()
        {
            Faculties = new HashSet<Faculty>();
        }

        public int CampusId { get; set; }
        public int UniversityId { get; set; }
        public string Location { get; set; }

        public virtual ICollection<Faculty> Faculties { get; set; }
        public virtual University University { get; set; }
    }
}
