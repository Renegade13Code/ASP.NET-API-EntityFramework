namespace webAPI.Models.DTO
{
    public class AddWalkRequest
    {
        public string Name { get; set; }
        public double Length { get; set; }
        public Guid WalkDifficultyId { get; set; }
        public Guid RegionId { get; set; }
    }
}
