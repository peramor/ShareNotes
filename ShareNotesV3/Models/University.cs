using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShareNotesV3.Models
{
    [Table("Universities")]
    public partial class University
    {
        public University()
        {
            Campus = new HashSet<Campus>();
        }

        public int UniversityId { get; set; }
        public string FullName { get; set; }
        public string ShortName { get; set; }

        public virtual ICollection<Campus> Campus { get; set; }
    }
}
