using Lawn_Mowing.Data;
using Lawn_Mowing.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace Lawn_Mowing.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;

        public AccountController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Login page
        [HttpGet]
        public IActionResult Login()
        {
            return View(new Account());  // Pass a new Account object for the form
        }

        // POST: Handle login
        [HttpPost]
        public IActionResult Login(Account account)
        {
            if (!ModelState.IsValid)
            {
                return View(account);  // Return the form if the model state is not valid
            }

            // Check if the account exists in the database
            var existingAccount = _context.Accounts
                .FirstOrDefault(a => a.Name == account.Name && a.Password == account.Password);

            if (existingAccount != null)
            {
                // Set a session variable to indicate the user is logged in
                HttpContext.Session.SetString("UserName", existingAccount.Name);

                // Redirect to the Bookings page upon successful login
                return RedirectToAction("Create", "Bookings");
            }

            // If login fails, add an error message to the model and return the view
            ModelState.AddModelError("", "Invalid username or password.");
            return View(account);
        }

        // Logout functionality
        public IActionResult Logout()
        {
            // Clear the session variable
            HttpContext.Session.Remove("UserName");

            // Redirect to the login page
            return RedirectToAction("Login");
        }
    }
}
