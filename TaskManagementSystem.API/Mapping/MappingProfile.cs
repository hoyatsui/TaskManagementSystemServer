using AutoMapper;
using TaskManagementSystem.Core.DTOs;
using TaskManagementSystem.Core.Models;

namespace TaskManagementSystem.API.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() { 
            CreateMap<Quote, QuoteDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<QuoteCreationDTO, Quote>().ReverseMap();
        }
    }
}
