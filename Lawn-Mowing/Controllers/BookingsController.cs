using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lawn_Mowing.Data;
using Lawn_Mowing.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Lawn_Mowing.Controllers
{
    public class BookingsController : Controller
    {
        private readonly AppDbContext _context;

        public BookingsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Bookings
        public async Task<IActionResult> Index()
        {
            var bookings = await _context.Bookings
                .Include(b => b.Customer)
                .Include(b => b.Operator)
                .Include(b => b.Machine)
                .ToListAsync();
            return View(bookings);
        }

        // GET: Bookings/Create
        public IActionResult Create()
        {
            // Populate dropdown lists with names, but send IDs as values
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Name");
            ViewData["OperatorId"] = new SelectList(_context.Operators, "Id", "Name"); // Needed for edit/view purposes
            ViewData["MachineId"] = new SelectList(_context.Machines, "Id", "Name");
            return View(new Booking()); // Return a new Booking object for the view
        }

        // POST: Bookings/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerId,MachineId,BookingDate,DurationInHours")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                // Check if the machine is already booked for the requested date
                var existingBooking = await _context.Bookings
                    .FirstOrDefaultAsync(b => b.MachineId == booking.MachineId && b.BookingDate == booking.BookingDate);

                if (existingBooking != null)
                {
                    // Redirect to the ConflictManager if the machine is already booked
                    return RedirectToAction("ConflictManager", new { id = existingBooking.Id });
                }

                // Retrieve the machine and check for assigned operator
                var machine = await _context.Machines.FindAsync(booking.MachineId);
                if (machine != null)
                {
                    // Assign the operator if the property exists in your Machine model
                    booking.OperatorId = machine.AssignedOperatorId;
                }

                // Validate the assigned OperatorId
                if (booking.OperatorId == null || !_context.Operators.Any(o => o.Id == booking.OperatorId))
                {
                    ModelState.AddModelError("", "The assigned operator does not exist or is not assigned.");
                }

                if (!ModelState.IsValid)
                {
                    // Repopulate the select lists if there was an error
                    ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Name", booking.CustomerId);
                    ViewData["OperatorId"] = new SelectList(_context.Operators, "Id", "Name"); // Ensure it's populated for any error messages
                    ViewData["MachineId"] = new SelectList(_context.Machines, "Id", "Name", booking.MachineId);
                    return View(booking);
                }

                // Set default status to "Pending"
                booking.Status = "Pending";

                // Add the booking to the database
                _context.Add(booking);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Operators", new { id = booking.OperatorId });
            }

            // Repopulate the select lists in case of validation failure
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Name", booking.CustomerId);
            ViewData["OperatorId"] = new SelectList(_context.Operators, "Id", "Name"); // Ensure it's populated for any error messages
            ViewData["MachineId"] = new SelectList(_context.Machines, "Id", "Name", booking.MachineId);
            return View(booking);
        }


        // GET: Bookings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }

            // Populate dropdown lists with names, but send IDs as values
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Name", booking.CustomerId);
            ViewData["OperatorId"] = new SelectList(_context.Operators, "Id", "Name", booking.OperatorId);
            ViewData["MachineId"] = new SelectList(_context.Machines, "Id", "Name", booking.MachineId);
            return View(booking);
        }

        // POST: Bookings/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CustomerId,OperatorId,MachineId,BookingDate,DurationInHours,Status")] Booking booking)
        {
            if (id != booking.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(booking);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingExists(booking.Id))
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

            // Repopulate the select lists in case of validation failure
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Name", booking.CustomerId);
            ViewData["OperatorId"] = new SelectList(_context.Operators, "Id", "Name", booking.OperatorId);
            ViewData["MachineId"] = new SelectList(_context.Machines, "Id", "Name", booking.MachineId);
            return View(booking);
        }

        // GET: Bookings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Bookings
                .Include(b => b.Customer)
                .Include(b => b.Operator)
                .Include(b => b.Machine)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookingExists(int id)
        {
            return _context.Bookings.Any(e => e.Id == id);
        }

        // Optionally, implement a ConflictManager action here for handling conflicts.
        public async Task<IActionResult> ConflictManager(int id)
        {
            var booking = await _context.Bookings
                .Include(b => b.Customer)
                .Include(b => b.Operator)
                .Include(b => b.Machine)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }
    }
}
