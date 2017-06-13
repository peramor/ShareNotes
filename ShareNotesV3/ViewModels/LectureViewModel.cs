using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShareNotesV3.ViewModels
{
    public class LectureViewModel
    {
        public int LectureId { get; set; }
        public DateTime Date { get; set; }
        public string Topic { get; set; }
        public string Abstract { get; set; }
        public List<NoteViewModel> Notes { get; set; }
    }
}
