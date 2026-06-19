using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace OakDevelopments.Models
{
    public class Booking
    {
        public int ID { get; set; }

        public int PropertyId { get; set; }

        [ValidateNever]
        public Property Property { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public DateTime RequestedDate { get; set; }

        public string Status { get; set; } = "Pending";

        public string? Comment { get; set; }
    }
}