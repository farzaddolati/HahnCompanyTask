
using AutoMapper;
using FarzadsTask.Domain;
using FarzadsTask.Dto;

namespace FarzadsTask.Mapping
{
    public class CityMappingProfile : Profile
    {
        public CityMappingProfile()
        {
            CreateMap<City, CityDto>().ReverseMap();
        }
    }
}
