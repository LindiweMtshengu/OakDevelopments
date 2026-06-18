namespace OakDevelopments.Models
{
    public class PropertyStatus
    {
        public int ID { get; set; }
        public string ? StatusName { get; set; }

        // Navigation
        public ICollection<Property> ? Properties { get; set; }

    }
}
