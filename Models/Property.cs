using System.ComponentModel.DataAnnotations;

namespace OakDevelopments.Models
{
    public class Property
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Title must be between 3 and 100 characters")]
        public string? Title { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [StringLength(500, MinimumLength = 10, ErrorMessage = "Description must be at least 10 characters")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(1, 100000000, ErrorMessage = "Price must be greater than 0")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Bedrooms are required")]
        [Range(1, 20, ErrorMessage = "Bedrooms must be at least 1")]
        public int Bedrooms { get; set; }

        [Required(ErrorMessage = "Suburb is required")]
        public string? Suburb { get; set; }

        [Required(ErrorMessage = "City is required")]
        public string? City { get; set; }

        [Required(ErrorMessage = "Province is required")]
        public string? Province { get; set; }

        // Foreign Keys
        public int AgentID { get; set; }
        public Agent? Agent { get; set; }

        public int PropertyTypeID { get; set; }
        public PropertyType? PropertyType { get; set; }

        public int StatusID { get; set; }
        public PropertyStatus? Status { get; set; }

        public string? Amenities { get; set; }

        // Navigation
        public List<PropertyImage>? Images { get; set; }
    }
}
