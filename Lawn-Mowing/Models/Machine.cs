using System.Collections.Generic;

namespace Lawn_Mowing.Models
{
    public class Machine
    {
        public int Id { get; set; }

        // Initialize properties with default values to avoid null references
        public string Name { get; set; } = string.Empty; // Default initialization
        public string Description { get; set; } = string.Empty; // Default initialization
        public bool IsAvailable { get; set; } = true; // Default value for availability
        public int AssignedOperatorId { get; set; } // Foreign key for the operator
        public virtual Operator AssignedOperator { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
