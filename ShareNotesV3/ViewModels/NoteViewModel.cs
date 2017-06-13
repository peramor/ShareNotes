using ShareNotesV3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShareNotesV3.ViewModels
{
    public class NoteViewModel
    {
        public int NoteId { get; set; }
        public string StudentName { get; set; }
        public double Rating { get; set; }
        public string Document { get; set; }
        public int Views { get; set; }
        public int MyRate { get; set; }
        public List<Marker> Markers { get; set; }
    }
}
