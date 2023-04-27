
using AutoMapper;
using FarzadsTask.Domain;
using FarzadsTask.Dto;

namespace FarzadsTask.Mapping
{
    public class CustomerMappingProfile : Profile
    {
        public CustomerMappingProfile()
        {
            CreateMap<Customer, CustomerDto>().ReverseMap();
        }
    }
}
