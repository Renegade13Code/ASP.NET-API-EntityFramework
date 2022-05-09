namespace webAPI.Models.Domain
{
    public class Region
    {
        public Guid RegionId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public double Area { get; set; }
        public double Lat { get; set; }
        public double Long { get; set; }
        public long Population { get; set; }

        // Navigation properties
        // These are properties that tell Entity the connection between the models
        
        public IEnumerable<Walk> Walks { get; set; } //This will tell entity framework that one region can have multiple walks inside it 
    }
}
