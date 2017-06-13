using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShareNotesV3.Models;
using ShareNotesV3.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace ShareNotesV3.Controllers
{
    public class NotesController : Controller
    {
        private readonly ShareNotes_dbContext _context;

        static CloudBlobClient blobClient;
        const string blobContainerName = "lectures";
        static CloudBlobContainer blobContainer;

        public NotesController(ShareNotes_dbContext context)
        {
            _context = context;
        }

        // GET: Notes
        public async Task<IActionResult> Index()
        {
            var shareNotes_dbContext = _context.Notes.Include(n => n.Lecture).Include(n => n.Student);
            return View(await shareNotes_dbContext.ToListAsync());
        }

        // GET: Notes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var note = await _context.Notes
                .Include(n => n.Lecture)
                .Include(n => n.Student)
                .SingleOrDefaultAsync(m => m.NoteId == id);
            if (note == null)
            {
                return NotFound();
            }

            return View(note);
        }

        // GET: Notes/Create
        public IActionResult Create()
        {
            ViewData["LectureId"] = new SelectList(_context.Lectures, "LectureId", "Topic");
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "FullName");
            return View();
        }

        // POST: Notes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(List<IFormFile> files, [Bind("NoteId,LectureId,StudentId,Document")] Note note)
        {
            try
            {
                if (files.Count > 0)
                {
                    var img = CombineBitmap(files);

                    CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=pasmstorage;AccountKey=xfaTgDlfKq+APm3WvI+oYkSZE4qMUerUuoe4YHP977hVjeWmp1Ki0IWC/c5Fnvf4gR6tadsm9oDl3JHPqQeKKg==;EndpointSuffix=core.windows.net");

                    // Create a blob client for interacting with the blob service.
                    blobClient = storageAccount.CreateCloudBlobClient();
                    blobContainer = blobClient.GetContainerReference(blobContainerName);
                    await blobContainer.CreateIfNotExistsAsync();

                    var fileName = GetRandomBlobName(files[0].FileName);
                    CloudBlockBlob blob = blobContainer.GetBlockBlobReference(fileName);
                    var bytes = ImageToByte(img);
                    await blob.UploadFromByteArrayAsync(bytes,0,bytes.Count());

                    note.Document = blob.StorageUri.PrimaryUri.AbsoluteUri;
                    _context.Notes.Add(note);
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewData["message"] = ex.Message;
                ViewData["trace"] = ex.StackTrace;
                return View("Error");
            }
        }

        // GET: Notes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var note = await _context.Notes.SingleOrDefaultAsync(m => m.NoteId == id);
            if (note == null)
            {
                return NotFound();
            }
            ViewData["LectureId"] = new SelectList(_context.Lectures, "LectureId", "Topic", note.LectureId);
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "FullName", note.StudentId);
            return View(note);
        }

        // POST: Notes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NoteId,LectureId,StudentId,Document")] Note note)
        {
            if (id != note.NoteId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(note);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NoteExists(note.NoteId))
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
            ViewData["LectureId"] = new SelectList(_context.Lectures, "LectureId", "Topic", note.LectureId);
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "FullName", note.StudentId);
            return View(note);
        }

        // GET: Notes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var note = await _context.Notes
                .Include(n => n.Lecture)
                .Include(n => n.Student)
                .SingleOrDefaultAsync(m => m.NoteId == id);
            if (note == null)
            {
                return NotFound();
            }

            return View(note);
        }

        // POST: Notes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var note = await _context.Notes.SingleOrDefaultAsync(m => m.NoteId == id);
            _context.Notes.Remove(note);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool NoteExists(int id)
        {
            return _context.Notes.Any(e => e.NoteId == id);
        }

        /// <summary> 
        /// string GetRandomBlobName(string filename): Generates a unique random file name to be uploaded  
        /// </summary> 
        private string GetRandomBlobName(string filename)
        {
            string ext = Path.GetExtension(filename);
            return string.Format("{0:10}_{1}{2}", DateTime.Now.Ticks, Guid.NewGuid(), ext);
        }

        public Bitmap CombineBitmap(List<IFormFile> files)
        {
            List<Bitmap> images = new List<Bitmap>();
            Bitmap finalImage = null;

            try
            {
                int width = 0;
                int height = 0;

                foreach (var file in files)
                {
                    Bitmap bitmap = new Bitmap(Image.FromStream(file.OpenReadStream()));

                    width = bitmap.Width > width ? bitmap.Width : width;
                    height += bitmap.Height;

                    images.Add(bitmap);
                }

                finalImage = new Bitmap(width, height);

                using (Graphics g = Graphics.FromImage(finalImage))
                {
                    g.Clear(Color.White);

                    int offset = 0;
                    foreach (Bitmap image in images)
                    {
                        g.DrawImage(image,
                          new Point(0, offset));
                        offset += image.Height;
                    }
                }

                return finalImage;
            }
            catch (Exception ex)
            {
                if (finalImage != null)
                    finalImage.Dispose();

                throw ex;
            }
            finally
            {
                foreach (Bitmap image in images)
                {
                    image.Dispose();
                }
            }
        }

        public static byte[] ImageToByte(Image img)
        {
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(img, typeof(byte[]));
        }
    }
}
