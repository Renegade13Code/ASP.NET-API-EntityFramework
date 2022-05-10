namespace webAPI.Models.DTO
{
    public class WalkDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Length { get; set; }
        public Guid WalkDifficultyId { get; set; }
        public Guid RegionId { get; set; }

        //Navigational properties
        //NB! This needs to be the RegionDTO, otherwise when serializing the walks data circular object referencing will occur. 
        //This is becuase the region domain model references the walks domain model, whereas the region DTO model does not.
        public RegionDTO Region { get; set; }
        public WalkDifficultyDTO WalkDifficulty { get; set; }
    }
}
