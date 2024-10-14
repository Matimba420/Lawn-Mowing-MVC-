using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Lawn_Mowing.Models;

namespace Lawn_Mowing.Data
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Operator> Operators { get; set; }
        public DbSet<Machine> Machines { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<ConflictManager> ConflictManagers { get; set; }
        public DbSet<Account> Accounts { get; set; } // Add Accounts DbSet

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed Machines
            modelBuilder.Entity<Machine>().HasData(
                new Machine { Id = 1, Name = "TurboMower 224", Description = "High-speed lawn mower", IsAvailable = true },
                new Machine { Id = 2, Name = "PowerCutter 112", Description = "Standard lawn mower", IsAvailable = true }
            );

            // Seed Operators
            modelBuilder.Entity<Operator>().HasData(
                new Operator { Id = 1, Name = "John Doe" },
                new Operator { Id = 2, Name = "Jane Smith" }
            );

            // Seed Conflict Managers
            modelBuilder.Entity<ConflictManager>().HasData(
                new ConflictManager { Id = 1, Name = "Conflict Manager Mike" }
            );

            

            // Seed Accounts (Directly add user accounts)
            modelBuilder.Entity<Account>().HasData(
                new Account { Id = 1, Name = "User1", Password = "password1" }, // Use hashed passwords in production
                new Account { Id = 2, Name = "User2", Password = "password2" }
            );
        }
    }
}
