namespace OakDevelopments.Models
{
    public class Property
    {
        public int Id { get; set; } // PK
        public string Type { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Location { get; set; }
    }
}
