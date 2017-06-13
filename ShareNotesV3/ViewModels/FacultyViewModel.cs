using ShareNotesV3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShareNotesV3.ViewModels
{
    public class FacultyViewModel
    {
        public string Name { get; set; }
        public List<CourseViewModel> Courses { get; set; }
    }
}
