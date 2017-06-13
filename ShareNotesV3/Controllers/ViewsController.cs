using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShareNotesV3.Models;

namespace ShareNotesV3.Controllers
{
    public class ViewsController : Controller
    {
        private readonly ShareNotes_dbContext _context;

        public ViewsController(ShareNotes_dbContext context)
        {
            _context = context;    
        }

        // POST: Views/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public JsonResult Rate(int rate, int noteId)
        {
            var view = _context.Views.SingleOrDefault(v => v.NoteId == noteId
                            && v.StudentId == int.Parse(Request.Cookies["studentId"]));
            view.Rate = rate;
            _context.SaveChanges();
            var average = _context.Views.Where(v => v.NoteId == noteId).Average(v => v.Rate);
            return Json(new { Rate = rate, Note = noteId, Average = average });
        }


        private bool ViewExists(int id)
        {
            return _context.Views.Any(e => e.NoteId == id);
        }
    }
}
