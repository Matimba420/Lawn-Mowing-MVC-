using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lawn_Mowing.Models
{
    public class Operator
    {
        public int Id { get; set; }

        [Required] // Ensure that the Name is required
        public string Name { get; set; } = string.Empty; // Initialize with a default value

        // A list of machine IDs that this operator can operate
        public List<int> OperableMachineIds { get; set; } = new List<int>();

        public int? AssignedMachineName { get; set; }

        // Method to check if the operator can operate a specific machine
        public bool CanOperateMachine(int machineId)
        {
            return OperableMachineIds.Contains(machineId);
        }

        // Navigation property to bookings
        public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>(); // Initialize to avoid null reference
    }
}