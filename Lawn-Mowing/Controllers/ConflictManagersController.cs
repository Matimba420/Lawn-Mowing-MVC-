using Lawn_Mowing.Data;
using Lawn_Mowing.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Lawn_Mowing.Controllers
{
    public class ConflictManagersController : Controller
    {
        private readonly AppDbContext _context;

        public ConflictManagersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ConflictManagers
        public async Task<IActionResult> Index()
        {
            var conflictManagers = await _context.ConflictManagers.ToListAsync();
            return View(conflictManagers);
        }

        // GET: ConflictManagers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ConflictManagers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ConflictManager conflictManager)
        {
            if (ModelState.IsValid)
            {
                _context.Add(conflictManager);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(conflictManager);
        }

        // GET: ConflictManagers/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var conflictManager = await _context.ConflictManagers.FindAsync(id);
            if (conflictManager == null)
            {
                return NotFound();
            }
            return View(conflictManager);
        }

        // POST: ConflictManagers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var conflictManager = await _context.ConflictManagers.FindAsync(id);
            if (conflictManager != null)
            {
                _context.ConflictManagers.Remove(conflictManager);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: ConflictManagers/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var booking = await _context.Bookings
                .Include(b => b.Customer)
                .Include(b => b.Machine)
                .Include(b => b.Operator)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }
    }
}
