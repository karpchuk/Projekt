using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using BeFit.Data;
using BeFit.Models;
using BeFit.DTOs;

namespace BeFit.Controllers
{
    [Authorize] // ca?y controller dost?pny tylko dla zalogowanych
    public class ExerciseTypesController : Controller
    {
        private readonly ApplicationDbContext _context;
    public ExerciseTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        private string GetUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
        }

        // GET: ExerciseTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.ExerciseType.ToListAsync());
        }

        // GET: ExerciseTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var exerciseType = await _context.ExerciseType
                .FirstOrDefaultAsync(m => m.Id == id);

            if (exerciseType == null)
                return NotFound();

            return View(exerciseType);
        }

        // GET: ExerciseTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ExerciseTypes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] ExerciseTypeDTO exerciseTypeDTO)
        {
            if (!ModelState.IsValid)
                return View(exerciseTypeDTO);

            ExerciseType exerciseType = new ExerciseType()
            {
                Name = exerciseTypeDTO.Name
            };

            _context.Add(exerciseType);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: ExerciseTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var exerciseType = await _context.ExerciseType.FindAsync(id);

            if (exerciseType == null)
                return NotFound();

            return View(exerciseType);
        }

        // POST: ExerciseTypes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] ExerciseTypeDTO exerciseTypeDTO)
        {
            if (id != exerciseTypeDTO.Id)
                return NotFound();

            if (!ModelState.IsValid)
                return View(exerciseTypeDTO);

            var exerciseType = await _context.ExerciseType.FindAsync(id);
            if (exerciseType == null)
                return NotFound();

            exerciseType.Name = exerciseTypeDTO.Name;

            _context.Update(exerciseType);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: ExerciseTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var exerciseType = await _context.ExerciseType
                .FirstOrDefaultAsync(m => m.Id == id);

            if (exerciseType == null)
                return NotFound();

            return View(exerciseType);
        }

        // POST: ExerciseTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var exerciseType = await _context.ExerciseType.FindAsync(id);

            if (exerciseType != null)
                _context.ExerciseType.Remove(exerciseType);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool ExerciseTypeExists(int id)
        {
            return _context.ExerciseType.Any(e => e.Id == id);
        }
    }

}
