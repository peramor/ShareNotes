using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShareNotesV3.Models;
using System.Security.Cryptography;
using System.Text;

namespace ShareNotesV3.Controllers
{
    public class StudentsController : Controller
    {
        private readonly ShareNotes_dbContext _context;

        public StudentsController(ShareNotes_dbContext context)
        {
            _context = context;    
        }

        // GET: Students
        public async Task<IActionResult> Index()
        {
            var shareNotes_dbContext = _context.Students.Include(s => s.Faculty);
            return View(await shareNotes_dbContext.ToListAsync());
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details()
        {
            int studentId = 0;
            int.TryParse(Request.Cookies["studentId"], out studentId);

            if (studentId == 0)
                return RedirectToRoute(new
                {
                    controller = "Home",
                    action = "Index"
                });

            var student = await _context.Students
                .Include(s => s.Faculty)
                .SingleOrDefaultAsync(m => m.StudentId == studentId);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            if (!String.IsNullOrEmpty(Request.Cookies["studentId"]))
                return SignOut();
            ViewData["FacultyId"] = new SelectList(_context.Faculties, "FacultyId", "Name");
            return View();
        }


        public IActionResult SignOut()
        {
            Response.Cookies.Delete("studentId");
            return RedirectToRoute(new
            {
                controller = "Home",
                action = "Index"
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("StudentId,FacultyId,FullName,Mail,Pwd")] Student student)
        {
            if (ModelState.IsValid)
            {
                var authStudent = await _context.Students.SingleOrDefaultAsync(s => s.Pwd == CalculateMD5Hash(student.Pwd));
                if (authStudent != null)
                {
                    Response.Cookies.Append("studentId", authStudent.StudentId.ToString());
                    return RedirectToRoute(new
                    {
                        controller = "Home",
                        action = "Index"
                    });
                }
                else
                {
                    ViewData["FacultyId"] = new SelectList(_context.Faculties, "FacultyId", "Name", student.FacultyId);
                    return RedirectToAction("Create");
                }
            }
            ViewData["FacultyId"] = new SelectList(_context.Faculties, "FacultyId", "Name", student.FacultyId);
            return RedirectToAction("Create");
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudentId,FacultyId,FullName,Mail,Pwd")] Student student)
        {
            if (ModelState.IsValid)
            {
                student.Pwd = CalculateMD5Hash(student.Pwd);
                _context.Students.Add(student);
                await _context.SaveChangesAsync();

                Response.Cookies.Append("studentId", student.StudentId.ToString());
                return RedirectToRoute(new
                {
                    controller = "Home",
                    action = "Index"
                });
            }
            ViewData["FacultyId"] = new SelectList(_context.Faculties, "FacultyId", "Name", student.FacultyId);
            return View(student);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students.SingleOrDefaultAsync(m => m.StudentId == id);
            if (student == null)
            {
                return NotFound();
            }
            ViewData["FacultyId"] = new SelectList(_context.Faculties, "FacultyId", "Name", student.FacultyId);
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StudentId,FacultyId,FullName,Mail,Pwd")] Student student)
        {
            if (id != student.StudentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.StudentId))
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
            ViewData["FacultyId"] = new SelectList(_context.Faculties, "FacultyId", "Name", student.FacultyId);
            return View(student);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(s => s.Faculty)
                .SingleOrDefaultAsync(m => m.StudentId == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Students.SingleOrDefaultAsync(m => m.StudentId == id);
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.StudentId == id);
        }

        private static string CalculateMD5Hash(string input)
        {
            MD5 md5 = MD5.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }

            return sb.ToString();
        }
    }
}
