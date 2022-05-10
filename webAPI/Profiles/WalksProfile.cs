using AutoMapper;

namespace webAPI.Profiles
{
    public class WalksProfile : Profile
    {
        public WalksProfile()
        {
            CreateMap<Models.Domain.Walk, Models.DTO.WalkDTO>().ReverseMap();

            // Mapping for walkdifficulty included here because walkdifficulty relates to walks
            CreateMap<Models.Domain.WalkDifficulty, Models.DTO.WalkDifficultyDTO>().ReverseMap();
        }
    }
}
