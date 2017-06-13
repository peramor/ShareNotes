using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShareNotesV3.ViewModels
{
    public class CourseViewModel
    {
        public string Name { get; set; }
        public List<LectureViewModel> Lectures { get; set; }
    }
}
