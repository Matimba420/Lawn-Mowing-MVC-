using System;
using System.ComponentModel.DataAnnotations;

namespace Lawn_Mowing.Models
{
    public class Booking
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Customer is required")]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Operator is required")]
        public int OperatorId { get; set; }

        [Required(ErrorMessage = "Machine is required")]
        public int MachineId { get; set; }

        [Required(ErrorMessage = "Booking Date is required")]
        [DataType(DataType.Date)]
        [Display(Name = "Booking Date")]
        public DateTime BookingDate { get; set; }

        // New property for duration in hours
        [Required(ErrorMessage = "Duration in hours is required")]
        [Range(1, 24, ErrorMessage = "Duration must be between 1 and 24 hours")]
        public int DurationInHours { get; set; }

        // New property for status of the booking
        public string Status { get; set; } = "Pending"; // Default status

        // Mark these properties as nullable to avoid CS8618 warnings
        public virtual Customer? Customer { get; set; }
        public virtual Operator? Operator { get; set; }
        public virtual Machine? Machine { get; set; }
    }
}
