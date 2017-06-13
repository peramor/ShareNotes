using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShareNotesV3.Models
{
    [Table("Markers")]
    public partial class Marker
    {
        public int MarkerId { get; set; }
        public string Label { get; set; }
        public int StudentId { get; set; }
        public int NoteId { get; set; }
        public int StartY { get; set; }
        public int EndY { get; set; }

        public virtual Note Note { get; set; }
        public virtual Student Student { get; set; }

        public RouteValueDictionary RouteValues
        {
            get
            {
                var rvd = new RouteValueDictionary();
                rvd["MarkerId"] = MarkerId;
                rvd["Label"] = Label;
                rvd["StudentId"] = StudentId;
                rvd["NoteId"] = NoteId;
                rvd["StartY"] = StartY;
                rvd["EndY"] = EndY;
                return rvd;
            }
        }
    }
}
