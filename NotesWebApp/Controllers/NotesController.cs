using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NotesWebApp.Data;
using NotesWebApp.Models;

namespace NotesWebApp.Controllers
{
    public class NotesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NotesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Notes
        public async Task<IActionResult> Index()
        {
            return _context.NotesModel != null ?
                        View(await _context.NotesModel.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.NotesModel'  is null.");
        }

        public async Task<IActionResult> ShowSearchForm()
        {
            return View();
        }

        public async Task<IActionResult> ShowSearchResults(String SearchPhrase)
        {
            return View("Index", await _context.NotesModel.Where(n => n.NotesTitle.Contains(SearchPhrase)).ToListAsync());
        }


        // GET: Notes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.NotesModel == null)
            {
                return NotFound();
            }

            var notesModel = await _context.NotesModel
                .FirstOrDefaultAsync(m => m.id == id);
            if (notesModel == null)
            {
                return NotFound();
            }

            return View(notesModel);
        }

        // GET: Notes/Create
        [Authorize] 
        public IActionResult Create()
        {
            return View();
        }

        // POST: Notes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,NoteAuthor,NotesTitle,NotesDescription")] NotesModel notesModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(notesModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(notesModel);
        }
        

        // GET: Notes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.NotesModel == null)
            {
                return NotFound();
            }

            var notesModel = await _context.NotesModel.FindAsync(id);
            if (notesModel == null)
            {
                return NotFound();
            }
            return View(notesModel);
        }

        // POST: Notes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,NoteAuthor,NotesTitle,NotesDescription")] NotesModel notesModel)
        {
            if (id != notesModel.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(notesModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NotesModelExists(notesModel.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(notesModel);
        }

        // GET: Notes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.NotesModel == null)
            {
                return NotFound();
            }

            var notesModel = await _context.NotesModel
                .FirstOrDefaultAsync(m => m.id == id);
            if (notesModel == null)
            {
                return NotFound();
            }

            return View(notesModel);
        }

        // POST: Notes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.NotesModel == null)
            {
                return Problem("Entity set 'ApplicationDbContext.NotesModel'  is null.");
            }
            var notesModel = await _context.NotesModel.FindAsync(id);
            if (notesModel != null)
            {
                _context.NotesModel.Remove(notesModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NotesModelExists(int id)
        {
          return (_context.NotesModel?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
