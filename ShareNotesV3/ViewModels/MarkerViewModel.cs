using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShareNotesV3.ViewModels
{
    public class MarkerViewModel
    {
        public string Label { get; set; }
        public int StartY { get; set; }
        public int EndY { get; set; }
        public string Document { get; set; }
        public int MarkerId { get; set; }
        public int NoteId { get; set; }
    }
}
