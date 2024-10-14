using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lawn_Mowing.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        public string Phone { get; set; }

        public string Password { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
