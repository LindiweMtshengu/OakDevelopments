namespace OakDevelopments.Models
{
    public class PropertyImage
    {
        public int ID { get; set; }                // PK
        public int PropertyID { get; set; }
        public string ? ImagePath { get; set; }

        // Navigation
        public Property ? Property { get; set; }

    }
}
