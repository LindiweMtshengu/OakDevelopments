namespace OakDevelopments.Models
{
    public class Agent
    {
        public int ID { get; set; }
        public string ? FirstName { get; set; }
        public string ? LastName { get; set; }
        public string ? Email { get; set; }
        public string ? Phone { get; set; }
        public string ? Suburb { get; set; }
        public string ? City { get; set; }
        public string ? Province { get; set; }
        public string ? ProfilePhoto { get; set; }

        // Navigation
        public ICollection<Property> ? Properties { get; set; }

    }
}
