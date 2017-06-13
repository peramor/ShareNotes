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
    public class LecturesController : Controller
    {
        private readonly ShareNotes_dbContext _context;

        public LecturesController(ShareNotes_dbContext context)
        {
            _context = context;    
        }

        // GET: Lectures
        public async Task<IActionResult> Index()
        {
            var shareNotes_dbContext = _context.Lectures.Include(l => l.Course);
            return View(await shareNotes_dbContext.ToListAsync());
        }

        // GET: Lectures/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lecture = await _context.Lectures
                .Include(l => l.Course)
                .SingleOrDefaultAsync(m => m.LectureId == id);
            if (lecture == null)
            {
                return NotFound();
            }

            return View(lecture);
        }

        // GET: Lectures/Create
        public IActionResult Create()
        {
            ViewData["CourseId"] = new SelectList(_context.Courses, "CourseId", "Name");
            return View();
        }

        // POST: Lectures/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LectureId,CourseId,Date,Topic,Abstract")] Lecture lecture)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lecture);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "CourseId", "Name", lecture.CourseId);
            return View(lecture);
        }

        // GET: Lectures/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lecture = await _context.Lectures.SingleOrDefaultAsync(m => m.LectureId == id);
            if (lecture == null)
            {
                return NotFound();
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "CourseId", "Name", lecture.CourseId);
            return View(lecture);
        }

        // POST: Lectures/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LectureId,CourseId,Date,Topic,Abstract")] Lecture lecture)
        {
            if (id != lecture.LectureId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lecture);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LectureExists(lecture.LectureId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "CourseId", "Name", lecture.CourseId);
            return View(lecture);
        }

        // GET: Lectures/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lecture = await _context.Lectures
                .Include(l => l.Course)
                .SingleOrDefaultAsync(m => m.LectureId == id);
            if (lecture == null)
            {
                return NotFound();
            }

            return View(lecture);
        }

        // POST: Lectures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lecture = await _context.Lectures.SingleOrDefaultAsync(m => m.LectureId == id);
            _context.Lectures.Remove(lecture);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool LectureExists(int id)
        {
            return _context.Lectures.Any(e => e.LectureId == id);
        }
    }
}
