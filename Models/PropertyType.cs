namespace OakDevelopments.Models
{
    public class PropertyType
    {
        public int Id { get; set; }
        public string ? Name { get; set; }

        // Navigation property back to Properties
        public ICollection<Property>? Properties { get; set; }
    }
}