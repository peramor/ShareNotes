using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShareNotesV3.Models;
using Microsoft.EntityFrameworkCore;
using ShareNotesV3.ViewModels;

namespace ShareNotesV3.Controllers
{
    public class HomeController : Controller
    {
        private readonly ShareNotes_dbContext _context;

        public HomeController(ShareNotes_dbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index() // Бизнес-информатика.
        {
            int studentId = 0;
            int.TryParse(Request.Cookies["studentId"], out studentId);

            if (studentId == 0)
                return View();

            var id = _context.Students.SingleOrDefault(s => s.StudentId == studentId).FacultyId;

            if (!await _context.Courses.AnyAsync(c => c.CourseId == id))
                return NotFound();

            var courses = from f in _context.Faculties
                          where f.FacultyId == id
                          select new FacultyViewModel
                          {
                              Name = f.Name,
                              Courses = (from c in _context.Courses
                                         where c.Faculty == f
                                         select new CourseViewModel
                                         {
                                             Name = c.Name,
                                             Lectures = (from l in _context.Lectures.Include(l => l.Notes)
                                                         where l.Course == c
                                                         select new LectureViewModel
                                                         {
                                                             Topic = l.Topic,
                                                             Date = l.Date,
                                                             Abstract = l.Abstract,
                                                             LectureId = l.LectureId,
                                                             Notes = (from n in _context.Notes
                                                                      where n.LectureId == l.LectureId
                                                                      select new NoteViewModel
                                                                      {
                                                                          NoteId = n.NoteId,
                                                                          StudentName = n.Student.FullName,
                                                                          Rating = Math.Round(n.Views.Average(v => v.Rate) == null ? 8 : n.Views.Average(v => v.Rate).Value,1),
                                                                          Markers = n.Markers.ToList()
                                                                      }).OrderByDescending(n => n.Rating).ToList()
                                                         }).ToList()

                                         }).ToList()
                          };

            return View(await courses.SingleOrDefaultAsync());
        }

        public async Task<IActionResult> About(int? id)
        {
            NoteViewModel note;
            if (id == null)
                note = new NoteViewModel
                {
                    Document = "http://class-fizika.narod.ru/10_11_class/kinema/postusk2.jpg",
                    Rating = 3,
                    StudentName = "Mr.Robot",
                    Views = 228
                };
            else
            {
                var noteQuery = from n in _context.Notes.Include(n => n.Views).Include(n => n.Markers)
                                where n.NoteId == id
                                select new NoteViewModel
                                {
                                    NoteId = n.NoteId,
                                    Document = n.Document,
                                    StudentName = n.Student.FullName,
                                    Rating = Math.Round(n.Views.Average(v => v.Rate) == null ? 8 : n.Views.Average(v => v.Rate).Value,1),
                                    Views = n.Views.Count,
                                    Markers = n.Markers.ToList()
                                };
                note = await noteQuery.SingleAsync();
            }

            try
            {
                _context.Views.Add(new Models.View
                {
                    NoteId = note.NoteId,
                    StudentId = int.Parse(Request.Cookies["studentId"])
                });
                await _context.SaveChangesAsync();
                note.Views++;
            }
            catch { }

            return View(note);
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
