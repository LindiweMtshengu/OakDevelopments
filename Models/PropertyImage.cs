namespace OakDevelopments.Models
{
    public class PropertyImage
    {
        public int ID { get; set; }                // PK
        public int PropertyID { get; set; }
        public string ImageUrl { get; set; }

        // Navigation
        public Property Property { get; set; }

    }
}
