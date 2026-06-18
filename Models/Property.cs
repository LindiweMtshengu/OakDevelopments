namespace OakDevelopments.Models
{
    public class Property
    {
        public int ID { get; set; }
        public string ? Title { get; set; }
        public string ? Description { get; set; }
        public decimal Price { get; set; }
        public int Bedrooms { get; set; }
        public string ? Suburb { get; set; }
        public string ? City { get; set; }
        public string ? Province { get; set; }



        // Foreign Keys
        public int AgentID { get; set; }
        public Agent ?Agent { get; set; }
        public int PropertyTypeID { get; set; }
        public PropertyType ? PropertyType { get; set; }
        public int StatusID { get; set; }
        public PropertyStatus ? Status { get; set; }

        // Navigation
        public List<PropertyImage> ? Images { get; set; }

    }
}
