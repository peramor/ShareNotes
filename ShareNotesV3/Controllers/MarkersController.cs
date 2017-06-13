using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShareNotesV3.Models;
using ShareNotesV3.ViewModels;

namespace ShareNotesV3.Controllers
{
    public class MarkersController : Controller
    {
        private readonly ShareNotes_dbContext _context;

        public MarkersController(ShareNotes_dbContext context)
        {
            _context = context;
        }

        // GET: Markers
        public async Task<IActionResult> Index(int lectureId)
        {
            var markers = (from n in _context.Notes.Include(n => n.Markers)
                           where n.LectureId == lectureId
                           from m in n.Markers
                           select new MarkerViewModel
                           {
                               Label = m.Label,
                               StartY = m.StartY,
                               EndY = m.EndY,
                               Document = n.Document,
                               MarkerId = m.MarkerId,
                               NoteId = n.NoteId
                           }).ToList();

            return View(markers);
        }

        // POST: Markers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public JsonResult Create(int startY, int endY, int noteId, string label)
        {
            Marker m = new Marker
            {
                StartY = startY,
                EndY = endY,
                NoteId = noteId,
                Label = label,
                StudentId = int.Parse(Request.Cookies["studentId"].ToString())
            };
            try
            {
                _context.Markers.Add(m);
                _context.SaveChanges();
                return Json(m);
            }
            catch
            {
                var json = Json("err");
                json.StatusCode = 500;
                return json;
            }
        }

        public JsonResult Delete(int markerId)
        {
            try
            {
                var markerToRemove = _context.Markers.Single(m => m.MarkerId == markerId);
                _context.Markers.Remove(markerToRemove);
                _context.SaveChanges();
                return Json(markerToRemove);
            }
            catch
            {
                var json = Json("err");
                json.StatusCode = 500;
                return json;
            }
        }

        private bool MarkerExists(int id)
        {
            return _context.Markers.Any(e => e.MarkerId == id);
        }
    }
}
