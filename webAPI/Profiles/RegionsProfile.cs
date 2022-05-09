using AutoMapper;

namespace webAPI.Profiles
{
    public class RegionsProfile: Profile
    {
        public RegionsProfile()
        {
            //Use this if fields in Domain match that of DTO
            //CreateMap<Models.Domain.Region, Models.DTO.Region>();

            //The ForMember method specifies how src field maps to destination field. The ReverseMap method allows for mapping in reverse if needed.
            CreateMap<Models.Domain.Region, Models.DTO.RegionDTO>()
                .ForMember(dest => dest.Id, options => options.MapFrom(src => src.RegionId))
                .ReverseMap();
        }
    }
}
